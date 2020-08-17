

using UnityEngine;

public class playerControls : MonoBehaviour
{
    public float forwardSpeed = 1f;
    public float sideSpeed = 4f;
   
    private float animeDuration = 2f;
    private float LANE_DISTANCE = 1f;
    //private Transform groundCheckPos; bool isGrounded; float radius = 0.3f; LayerMask layer;
    
    private Transform playerposition;
    //private Transform groundCheckPosi;
    //private float jumpForce=4f;
    //bool isGrounded;
    
   


    public CharacterController controller;

    void Start()
    {
        playerposition = gameObject.GetComponent<Transform>().transform;
    }

   /*private void OnCollisionEnter(Collision collision)              //ground checks OnCollisionEnter/Exit
    {
        if (collision.gameObject.tag == "Tile")
        {
            verticalVelocity = -0.5f;
            //isGrounded = true;
        }
    }*/
    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            //print("exited!");
            verticalVelocity -= gravity ;
            //isGrounded = false;
        }
    }*/

    void Update()
    {
        if (Time.time < animeDuration)
        {
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
            //controller.Move(Vector3.forward * forwardSpeed * Time.deltaTime);
            return;
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)               //Mouse Controls
            {
                // move.x = sideSpeed;
                transform.Translate(LANE_DISTANCE, 0, 0);

            }
            else
            {
                // move.x = -sideSpeed;
                transform.Translate(-LANE_DISTANCE, 0, 0);

            }
        }
        // move.x = Input.GetAxisRaw("Horizontal")*sideSpeed;
        if ((int)playerposition.position.z % 100 == 0)               //increasing speed
        {
            forwardSpeed += 0.25f;
        }

        transform.position += transform.forward * forwardSpeed * Time.deltaTime;
       
        
        
        
        //move.z = forwardSpeed;
        
        //controller.Move(move*Time.deltaTime);
        

    }
   


}
