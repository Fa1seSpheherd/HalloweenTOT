using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClickEventTrigger : MonoBehaviour
{
    public UnityEvent onMouseDownEvent;

    void Update()
    {
        // Controleer of de linker muisknop net is ingedrukt
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Als we op dit specifieke object klikken
                if (hit.transform == transform)
                {
                    onMouseDownEvent?.Invoke();
                }
            }
        }
    }
}