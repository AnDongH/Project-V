using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour {

    [SerializeField] private bool lookAtBaseObj = true;     
    [SerializeField] private Transform followedUI;         
    [SerializeField] private Transform baseObj;            
    [SerializeField] private bool rotateWithBase;       
    [SerializeField] private float followSpeed = 10f;     
    [SerializeField] private float distanceFromBase; 

    private void Update() {

        if (lookAtBaseObj) followedUI.rotation = Quaternion.LookRotation(followedUI.position - baseObj.position);

        if (rotateWithBase) {

            Vector3 targetDirection = Vector3.ProjectOnPlane(baseObj.forward, Vector3.up).normalized;
            Vector3 targetPosition = baseObj.position + targetDirection * distanceFromBase;

            targetPosition = Vector3.Lerp(followedUI.position, targetPosition, followSpeed * Time.deltaTime);
            targetPosition.y = followedUI.position.y;

            followedUI.position = targetPosition;
        }

    }

}
