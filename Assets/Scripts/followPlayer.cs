using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform cube1;
    public Vector3 offset;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = cube1.position + offset;
    }
}
