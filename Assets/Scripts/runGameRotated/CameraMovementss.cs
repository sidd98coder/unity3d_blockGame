
using UnityEngine;

public class CameraMovementss : MonoBehaviour
{

    public Transform PlayerTransform;
    //public Vector3 offset;
    public float smoothSpeed = 0.01f;
   
    

   
    void LateUpdate()
    {
        if (PlayerTransform)
        {
            Vector3 desiredPosition = PlayerTransform.TransformPoint(0, 10, -30);
            Vector3 smoothPosition = Vector3.Lerp(PlayerTransform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;


            transform.LookAt(PlayerTransform);
        }
        
    }
}
