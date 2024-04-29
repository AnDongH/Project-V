using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnQuestTrackerUI : MonoBehaviour
{
    [Header("Action Variable")]
    [SerializeField, Tooltip("Register the action you want.")] private InputActionReference actionReference;
    [SerializeField] private GameObject ui;

    // event register
    private void OnEnable() {
        actionReference.action.performed += On;
        actionReference.action.canceled += Off;
    }

    // event unregister
    private void OnDisable() {
        actionReference.action.performed -= On;
        actionReference.action.canceled -= Off;
    }

    private void On(InputAction.CallbackContext obj) {
        ui.GetComponent<CanvasGroup>().alpha = 1f;
    }

    private void Off(InputAction.CallbackContext obj) {
        ui.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
