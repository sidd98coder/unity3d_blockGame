using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float sideForce = 500f;
    public float upForce = 500f;
    // Start is called before the first frame update
    /*void Start()
    {
        
        
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(Input.GetKey("right"))
        {
            rb.AddForce(sideForce*Time.deltaTime,0,0);
        }
        if(Input.GetKey("left"))
        {
            rb.AddForce(-sideForce*Time.deltaTime,0,0);
        }
        if(Input.GetKey("up"))
        {
            rb.AddForce(0,0,forwardForce*Time.deltaTime);
        }
        if(Input.GetKey("down"))
        {
            rb.AddForce(0,0,-forwardForce*Time.deltaTime);
        }
        if(Input.GetKey("space"))
        {
            rb.AddForce(0,upForce*Time.deltaTime,0);
             rb.AddForce(0,-upForce*Time.deltaTime,0);
        }
    }
}
