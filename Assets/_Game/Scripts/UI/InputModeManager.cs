using UnityEngine;
using UnityEngine.EventSystems;

public class InputModeManager : MonoBehaviour
{
    public GameObject defaultButton; // Assign your first button here

    private Vector3 lastMousePosition;
    private bool isUsingGamepad = false;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Detect mouse movement
        if (Input.mousePosition != lastMousePosition)
        {
            if (isUsingGamepad)
            {
                // Switching from gamepad to mouse
                EventSystem.current.SetSelectedGameObject(null);
                isUsingGamepad = false;
            }
            lastMousePosition = Input.mousePosition;
        }

        // Detect gamepad usage (simple version)
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (!isUsingGamepad)
            {
                // Switching from mouse back to gamepad
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(defaultButton);
                }
            }
            isUsingGamepad = true;
        }
    }
}