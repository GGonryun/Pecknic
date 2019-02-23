﻿using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationDirection = 0f;
    public CharacterController controller;

    public Arm leftArm;
    public Arm rightArm;

    public Vector2 sensitivity = Vector2.zero;
    public VectorRange verticalRange = VectorRange.zero;

    float rotationY = 0F;


    void Awake()
    {
        leftArm.Collided += Destroy;
        rightArm.Collided += Destroy;
    }

    void OnEnable()
    {
        //spawner = EnemySpawner.Current;
    }

    void Update()
    {
        Movement();
        Punching();
        if (Input.GetKeyDown(KeyCode.P))
        {
            Seagull gull = SeagullSpawner.Current.Spawn();
            gull.gameObject.transform.position = new Vector3(10, 5, 10);
        }
    }

    void Punching()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            leftArm.Punch();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rightArm.Punch();
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity.x;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity.y;
        rotationY = Mathf.Clamp(rotationY, verticalRange.min, verticalRange.max);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Destroy(object sender, OnCollisionEventArgs e) 
    {
        e.Collider.gameObject.GetComponent<IDespawnable>().Despawn();
    }
}