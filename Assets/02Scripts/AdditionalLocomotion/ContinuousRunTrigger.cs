using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


// 현재 action -> 왼손 컨트롤러 조이스틱 클릭
public class ContinuousRunTrigger : MonoBehaviour
{
    [SerializeField] private InputActionReference actionReference;
    [SerializeField] private  ContinuousMoveProviderBase cmBase;
    [SerializeField] private float mulSpeed;

    private void OnEnable() {
        actionReference.action.performed += SetRun;
        actionReference.action.canceled += SetOrigin;
    }

    private void OnDisable() {
        actionReference.action.performed -= SetRun;
        actionReference.action.canceled -= SetOrigin;
    }

    private void SetRun(InputAction.CallbackContext obj) {
        if (mulSpeed != 0) cmBase.moveSpeed *= mulSpeed;
    }

    private void SetOrigin(InputAction.CallbackContext obj) {
        if (mulSpeed != 0) cmBase.moveSpeed /= mulSpeed;
    }
}
