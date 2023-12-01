using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private Transform m_playerTF;

    private void Awake()
    {
        InitializeReferences();
        FieldInitialization();
    }

    private void FieldInitialization()
    {
        m_playerWalkSpeed = m_playerStartingWalkSpeed;
        m_playerSprintSpeed = m_playerStartingSprintSpeed;
        m_currentPlayerStamina = m_playerStartingStamina;
    }

    private void InitializeReferences()
    {
#region Reference Initialization
        
        if(!m_playerTF)
        {
            m_playerTF = transform;
        }

        if(!m_mainCamera)
        {
            m_mainCamera = Camera.main;
        }
        
#endregion
    }

    private void Update()
    {

        MovePlayer();
        RotatePlayer();
        CameraMovement();

    }
    
    [Space(10)]
    [Header("Player Movement Settings")]
    [Space(5)]
    
    [SerializeField] private CharacterController m_playerCharacterController;
    [SerializeField] private float m_maxPlayerSpeed;
    [SerializeField] private float m_playerStartingWalkSpeed;
    [SerializeField] private float m_playerStartingSprintSpeed;
    private float m_playerWalkSpeed;
    private float m_playerSprintSpeed;
    private float m_currentPlayerSpeed;
    [SerializeField] private float m_gravityStrength = -9.81f;

    [Space(5)]
    [Header("Stamina Settings")]
    [Space(5)]
    [SerializeField] private float m_maxPlayerStamina;
    [SerializeField] private float m_playerStartingStamina;
    [SerializeField] private float m_playerStaminaDrainSpeed;
    [SerializeField] private float m_playerStaminaRecoverySpeed;
    
    private float m_currentPlayerStamina;

    private bool m_staminaHasToRecover;
    private bool m_canSprint;
    
    private Camera m_mainCamera;
    private void MovePlayer()
    {

        ClampMovementValues();

        Vector3 movement = GetCameraRelativeMovementVector3();

        movement.y = m_gravityStrength;

        if(m_currentPlayerStamina <= 0)
        {
            m_staminaHasToRecover = true;
        }

        if(m_currentPlayerStamina > 0 && !m_staminaHasToRecover)
        {
            m_canSprint = true;
        }
        else
        {
            m_canSprint = false;
        }
        
        if(m_currentPlayerStamina >= m_maxPlayerStamina)
        {
            m_staminaHasToRecover = false;
        }
        
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if(m_canSprint)
            {
                Sprint();
            }
            else
            {
                Walk();
            }
        }
        else
        {
            Walk();
        }
        
        m_playerCharacterController.Move(movement * (m_currentPlayerSpeed * Time.deltaTime));
    }

    private Vector3 GetCameraRelativeMovementVector3()
    {
        Transform m_cameraTF = m_mainCamera.transform;
        
        Vector3 forward = m_cameraTF.forward;
        Vector3 right = m_cameraTF.right;
        forward.y = 0;
        right.y = 0;
        
        Vector3 forwardRelativeVectorInput = InputManager.I.GetMovementVector2Normalized().y * forward;
        Vector3 rightRelativeVectorInput = InputManager.I.GetMovementVector2Normalized().x * right;

        Vector3 cameraRelativeMovement = forwardRelativeVectorInput + rightRelativeVectorInput;
        
        return cameraRelativeMovement;
    }

    private void ClampMovementValues()
    {
        m_currentPlayerSpeed = Mathf.Clamp(m_currentPlayerSpeed, 0, m_maxPlayerSpeed);
        m_currentPlayerStamina = Mathf.Clamp(m_currentPlayerStamina, 0, m_maxPlayerStamina);
    }

    private void Sprint()
    {
        m_currentPlayerSpeed = m_playerSprintSpeed;
        m_currentPlayerStamina -= m_playerStaminaDrainSpeed * Time.deltaTime;
    }

    private void Walk()
    {
        m_currentPlayerSpeed = m_playerWalkSpeed;
        m_currentPlayerStamina += m_playerStaminaRecoverySpeed * Time.deltaTime;
    }
    
    [Space(10)]
    [Header("Player Rotation Settings")]
    [Space(5)]

    [SerializeField] private float m_playerRotationSpeed;
    [SerializeField] private bool m_useRotationTime;
    private void RotatePlayer()
    {
        Vector3 mousePosition = InputManager.I.GetMousePositionInWorld();

        Vector3 lookDirection = mousePosition - m_playerTF.position;
        lookDirection.y = 0;
        
        Quaternion currentRotation = m_playerTF.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        if(lookDirection == Vector3.zero) return;
        
        m_playerTF.rotation = m_useRotationTime ? Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime / m_playerRotationSpeed) : targetRotation;

    }
    
    [Space(10)]
    [Header("Camera settings")]
    [Space(5)]
    
    [SerializeField] private CinemachineVirtualCamera m_playerCamera;
    [SerializeField] private float m_cameraRotationSpeed;

    private void CameraMovement()
    {
        var dolly = m_playerCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        
        if(Input.GetKey(KeyCode.Q))
        {
            dolly.m_PathPosition += m_cameraRotationSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.E))
        {
            dolly.m_PathPosition -= m_cameraRotationSpeed * Time.deltaTime;
        }
    }
}
