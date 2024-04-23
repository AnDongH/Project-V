using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour {

    [SerializeField] private bool m_LookAtCamera = true;     
    [SerializeField] private Transform m_UIElement;         
    [SerializeField] private Transform m_Camera;            
    [SerializeField] private bool m_RotateWithCamera;       
    [SerializeField] private float m_FollowSpeed = 10f;     
    [SerializeField] private float m_DistanceFromCamera; 



    private void Update() {

        if (m_LookAtCamera)
            m_UIElement.rotation = Quaternion.LookRotation(m_UIElement.position - m_Camera.position);


        if (m_RotateWithCamera) {

            Vector3 targetDirection = Vector3.ProjectOnPlane(m_Camera.forward, Vector3.up).normalized;
            Vector3 targetPosition = m_Camera.position + targetDirection * m_DistanceFromCamera;

            targetPosition = Vector3.Lerp(m_UIElement.position, targetPosition, m_FollowSpeed * Time.deltaTime);
            targetPosition.y = m_UIElement.position.y;

            m_UIElement.position = targetPosition;
        }
    }
}
