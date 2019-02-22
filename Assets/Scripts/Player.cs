﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationDirection = 0f;
    public CharacterController controller;

    void Update()
    {
        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
           

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        
        // rotate the player
        rotationDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationDirection);
        
        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}