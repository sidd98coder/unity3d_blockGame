
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    
    
    
    
    // Update is called once per frame
    void Update()
    {
        if(Player)
        {
            transform.position = Player.position + offset;
           

        }
       
    }
}
