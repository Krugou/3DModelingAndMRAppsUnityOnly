using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleObject : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    // Start is called before the first frame update

    private void Awake() {
        toggleReference.action.started += Toggle;
    }
    private void OnDestroy() {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context) {
        Debug.Log("Cylinder toggled!");
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
    }
}