using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour {

    [SerializeField] private bool PerfectFollow = true;     
    [SerializeField] private Transform followedUI;         
    [SerializeField] private Transform baseObj;                 
    [SerializeField] private float followSpeed = 10f;     
    [SerializeField] private float distanceFromBase; 

    private void Update() {
        followedUI.rotation = PerfectFollow ? Quaternion.LookRotation(baseObj.forward) : Quaternion.LookRotation(followedUI.position - baseObj.position);
        Vector3 targetDirection = PerfectFollow ? baseObj.forward : Vector3.ProjectOnPlane(baseObj.forward, Vector3.up).normalized;
        Vector3 targetPosition = baseObj.position + targetDirection * distanceFromBase;
        targetPosition = Vector3.Lerp(followedUI.position, targetPosition, followSpeed * Time.deltaTime);
        followedUI.position = targetPosition;
    }

}
