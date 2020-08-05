using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    public Text statusText;
    public Text scoreText;
    private Transform Playerpos;
    private float animeDuration = 2f;
    

    void Start()
    {
        Playerpos = this.gameObject.transform;
    }

    
    void Update()
    {
        if (Time.time < animeDuration)
        {
            statusText.text = "Get READY!!";
        }
        else
        {
            statusText.text = "";

        }
        scoreText.text = Playerpos.position.z.ToString("0");  //score
        /*if (Playerpos.position.y <= 1f )
        {
            print("Out Of Y Position !!!!");
            gameOver();
        }
        if(Playerpos.position.x < -4 || Playerpos.position.x > 4)
        {
            print("Out Of X Position !!!!");
            gameOver();
        }*/

    }
    private void OnCollisionEnter(Collision collision)  //collision
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            print("Collision occurs!!!!");
            gameOver();
        }
        /*if (collision.gameObject.tag == "Tile")
        {
            if ((gameObject.transform.position.y < collision.gameObject.transform.position.y) || (gameObject.transform.position.x) > 4 || (gameObject.transform.position.x) < -4)
            {
                print("Out Of Position !!!!");
                gameOver();
            }

        }*/
        
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            print("Out of X Position!!!!");
            gameOver();
        }
    }*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "groundCheck")
        {
            print("Out of Y position");
            gameOver();
        }
    }


    void gameOver()
    {
        this.gameObject.GetComponent<playerControls>().enabled = false;
        statusText.text = "Game Over!!";
        Destroy(this.gameObject, 1f);
        
    }
}
