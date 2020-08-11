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
    public Text highscore;
    int HighScore;
    
    //float forwardForce = 2000f;
    

    void Start()
    {
        highscore.text = "highscore : " + PlayerPrefs.GetInt("HighScore", 0).ToString("0");

        Playerpos = this.gameObject.transform;                                  //Timer In start

        //velocity = gameObject.GetComponent<Rigidbody>().velocity;
        StartCoroutine(getReady());
    }

    
    void Update()
    {
        /*if (Time.time < animeDuration)
        {
            statusText.text = "Get READY!!";                ///coroutine
        }
        else
        {
            if(gameObject)
                statusText.text = "";

        }*/
        scoreText.text = Playerpos.position.z.ToString("0");                //score
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
    private void OnCollisionEnter(Collision collision)                      //collision
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
        if ((int)Playerpos.position.z > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int)Playerpos.position.z);
            highscore.text = "highscore : " + Playerpos.position.z.ToString("0");
        }
        Destroy(this.gameObject, 2f);
        

    }
    IEnumerator getReady()
    {
        statusText.text = "Get Ready!!";
        yield return new WaitForSeconds(2f);
        statusText.text = "";
    }
}
