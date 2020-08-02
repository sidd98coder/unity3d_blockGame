using UnityEngine;

public class collision : MonoBehaviour
{
    public Controls movement;
    
    
    
   void OnCollisionEnter(Collision collisionInfo)
   {
       if(collisionInfo.collider.tag=="Obstacle")
       { 
          
           Debug.Log(collisionInfo.collider.name);
           
       }
   }
    
}
