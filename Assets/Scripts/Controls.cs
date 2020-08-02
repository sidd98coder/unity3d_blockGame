using UnityEngine;

public class Controls : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 4000f;
    public float sideForce = 120f;
    // Start is called before the first frame update
    /*void Start()
    {
        
        
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(Input.GetKey("right"))
        {
            rb.AddForce(sideForce*Time.deltaTime,0,0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("left"))
        {
            rb.AddForce(-sideForce*Time.deltaTime,0,0,ForceMode.VelocityChange);
        }
        if(Input.GetKey("up"))
        {
            rb.AddForce(0,0,forwardForce*Time.deltaTime);
        }
        if(Input.GetKey("down"))
        {
            rb.AddForce(0,0,-forwardForce*Time.deltaTime);
        }
    }
}
