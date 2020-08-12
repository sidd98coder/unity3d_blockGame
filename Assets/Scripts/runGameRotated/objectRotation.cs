
using UnityEngine;

public class objectRotation : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "candy")
        {
            transform.Rotate(0f,0f,10f,Space.Self);
        }
        else if(this.gameObject.tag== "coin")
        {
            transform.Rotate(10f, 0f, 0f, Space.Self);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
