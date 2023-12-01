using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private LayerMask m_interactLayerMask;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleInteract();
        }
    }

    private void HandleInteract()
    {
        Ray cameraToMousePositionRay = InputManager.I.m_MainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(cameraToMousePositionRay, out var hit, Mathf.Infinity, m_interactLayerMask))
        {
            if(hit.collider.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
