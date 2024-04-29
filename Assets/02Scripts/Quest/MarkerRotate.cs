using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatHeight = 0.5f;
    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
    }

    void Update() {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = initialPosition + new Vector3(0, newY, 0);
    }
}
