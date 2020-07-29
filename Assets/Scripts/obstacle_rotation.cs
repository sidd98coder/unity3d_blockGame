using UnityEngine;

public class obstacle_rotation : MonoBehaviour
{
     
    public GameObject target;

    void Update()
    {
       
        transform.RotateAround(target.transform.position, Vector3.up, 90 * Time.deltaTime);
    }
}
