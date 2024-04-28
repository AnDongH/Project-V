using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


// current action -> left controllor joy stick click
public class ContinuousRunTrigger : MonoBehaviour {

    [Header("Action Variable")]
    [SerializeField, Tooltip("Register the action you want.")] private InputActionReference actionReference;
    [SerializeField, Tooltip("This function is used only on continuous movement.")] private  ContinuousMoveProviderBase cmBase;
    [SerializeField, Tooltip("This value is multiplied by the basic moving speed.")] private float mulSpeed;

    // event register
    private void OnEnable() {
        actionReference.action.performed += SetRun;
        actionReference.action.canceled += SetOrigin;
    }

    // event unregister
    private void OnDisable() {
        actionReference.action.performed -= SetRun;
        actionReference.action.canceled -= SetOrigin;
    }

    private void SetRun(InputAction.CallbackContext obj) {

        if (mulSpeed == 0) {
            Define.LogError("multiplied value is zero");
            return;
        }

        if (cmBase == null) {
            Define.LogError("Move Provider is null");
            return;
        }

        cmBase.moveSpeed *= mulSpeed;
    }

    private void SetOrigin(InputAction.CallbackContext obj) {

        if (mulSpeed == 0) {
            Define.LogError("multiplied value is zero");
            cmBase.moveSpeed = 0;
            return;
        }

        if (cmBase == null) {
            Define.LogError("Move Provider is null");
            return;
        }

        cmBase.moveSpeed /= mulSpeed;
    }
}
