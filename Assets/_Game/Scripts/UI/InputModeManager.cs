using UnityEngine;
using UnityEngine.EventSystems;

public class InputModeManager : MonoBehaviour
{
    public GameObject defaultButton;

    private Vector3 lastMousePosition;
    private bool isUsingGamepad = false;
    private bool inputModeSet = false;

    void Update()
    {
        // Detect mouse movement
        if (Input.mousePosition != lastMousePosition)
        {
            if (!inputModeSet || isUsingGamepad)
            {
                // Switching to mouse
                EventSystem.current.SetSelectedGameObject(null);
                isUsingGamepad = false;
                inputModeSet = true;
            }
            lastMousePosition = Input.mousePosition;
        }

        // Detect gamepad usage
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (!isUsingGamepad || !inputModeSet)
            {
                // Switching to gamepad
                EventSystem.current.SetSelectedGameObject(defaultButton);
                isUsingGamepad = true;
                inputModeSet = true;
            }
        }
    }
}