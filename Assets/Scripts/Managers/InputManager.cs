using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager I {get; private set;}

    private InputActions m_inputActions;
    private InputAction m_playerMoveInputAction;
    
    public Camera m_MainCamera;

    private void Awake()
    {
        
#region Singleton

        if(I == null)
        {
            I = this;
        }
        else if(I != null && I != this)
        {
            Destroy(this.gameObject);
        }
        
        #endregion
        
        InitializeInputActions();
        
    }

    private void InitializeInputActions()
    {
        m_inputActions = new InputActions();
        m_playerMoveInputAction = m_inputActions.Player.Movement;
    }

    private void Start()
    {
        m_MainCamera = Camera.main;
    }

    [SerializeField] private LayerMask m_mousePositionMask;
    public Vector3 GetMousePositionInWorld()
    {
        Ray cameraRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(cameraRay, out var hit, Mathf.Infinity, m_mousePositionMask);

        return hit.point;
    }
    
    public Vector2 GetMovementVector2Normalized()
    {
        Vector2 movement = m_playerMoveInputAction.ReadValue<Vector2>();

        return movement;
    }

    private void OnEnable()
    {
        m_playerMoveInputAction.Enable();
    }

    private void OnDisable()
    {
        m_playerMoveInputAction.Disable();
    }
}
