using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController Player;
    public float playerSpeed=5.0f;
    public float gravity = -9.81f;
    Vector3 velocity;
    bool isGrounded=true;
    public Transform groundCheckObject;
    public LayerMask groundCheckLayer;
    public float sphereRadius=0.5f;
    public float jumpHeight = 0.1f;

    void Update()
    {
       isGrounded = Physics.CheckSphere(groundCheckObject.position, sphereRadius, groundCheckLayer);
        if (isGrounded && velocity.y<0)
        {
            velocity.y = 0f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Player.Move(move * playerSpeed * Time.deltaTime);
        //isGrounded = Physics.CheckSphere(groundCheckObject.position, groundDistance, groundCheckLayer);
        if (isGrounded && Input.GetButtonDown("Jump") )
        {
            velocity.y = Mathf.Sqrt(-2.0f * gravity * jumpHeight);
        }
         velocity.y += gravity*Time.deltaTime;
        Player.Move(velocity);

    }
}
