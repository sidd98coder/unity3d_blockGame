using UnityEngine;

public class Controls : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 4000f;
    public float sideForce = 100f;
    // Start is called before the first frame update
    /*void Start()
    {
        
        
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(sideForce*Time.deltaTime,0,0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-sideForce*Time.deltaTime,0,0,ForceMode.VelocityChange);
        }
        if(Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0,0,forwardForce*Time.deltaTime);
        }
        if(Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(0,0,-forwardForce*Time.deltaTime);
        }
    }
}
