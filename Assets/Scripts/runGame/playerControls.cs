using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    public float forwardSpeed = 1f;
    public float sideSpeed = 4f;
    float verticalVelocity=0.0f;
    float gravity = 9.81f;
    private float animeDuration = 2f;
    //private Transform groundCheckPos; bool isGrounded; float radius = 0.3f; LayerMask layer;
    private Vector3 move;
    //private Transform groundCheckPosi;
    
   


    public CharacterController controller;

    void Start()
    {
        //gameObject.transform.position = new Vector3(0, 1, 0);
        //groundCheckPos = gameObject.GetComponentInChildren<Transform>().transform;
        controller = GetComponent<CharacterController>();
        //groundCheckPosi = gameObject.GetComponentInChildren<Transform>().transform;
    }
    void Update()
    {
        if (Time.time < animeDuration)
        {
            controller.Move(Vector3.forward * forwardSpeed * Time.deltaTime);
            return;
        }

        /*isGrounded = Physics.CheckSphere(groundCheckPos.position,radius,layer);  //Ground Check
        if(verticalVelocity<0 && isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;  //Ground Check
        }
       */



        if(controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity*Time.deltaTime;
        }

        /*if (gameObject.transform.position.y>1)
        {
            move.y = -gameObject.transform.position.y;
            
        }
        else if (gameObject.transform.position.y == 1)
        {
            
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }*/

        move.x = Input.GetAxisRaw("Horizontal")*sideSpeed;
       /* if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)               //Mouse Controls
            {
                move.x = sideSpeed;
            }
            else
            {
                move.x = -sideSpeed;
            }
        }*/
        
        
        move.y = verticalVelocity;
        
        move.z = forwardSpeed;
        
        controller.Move(move*Time.deltaTime);
        

    }
}
