using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMotor : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed * Random.Range(0, Mathf.PI));
    }
}
