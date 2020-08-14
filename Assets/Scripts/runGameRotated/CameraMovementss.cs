
using UnityEngine;

public class CameraMovementss : MonoBehaviour
{
    //attached with Main Camera

    public Transform PlayerTransform;
    //public Vector3 offset;
    public float smoothSpeed = 0.01f;
   
    

   
    void LateUpdate()
    {
        if (PlayerTransform)
        {
            Vector3 desiredPosition = PlayerTransform.TransformPoint(0, 10, -30);       //camera always away from player by this distance on local axis
            Vector3 smoothPosition = Vector3.Lerp(PlayerTransform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;


            transform.LookAt(PlayerTransform);
        }
        
    }
}
