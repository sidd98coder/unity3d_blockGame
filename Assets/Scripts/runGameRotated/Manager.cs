using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    int totalCoin=0;
    int totalCandy = 0;
    public Text coin;
    public Text candy;
    public Text score;
    public Text statusText;
    public Text highscore;
    private float finalscore;

    private void Start()
    {
        highscore.text = "HIGHSCORE : " + PlayerPrefs.GetInt("R_highScore", 0).ToString("0");
        StartCoroutine(getReady());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            totalCoin++;
            coin.text = totalCoin.ToString();
        }else if (other.gameObject.tag == "candy")
        {
            totalCandy++;
            candy.text = totalCandy.ToString();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            gameOver();
        }
    }
    private void Update()
    {
        if (gameObject)
        {
            if (gameObject.transform.position.y < -2)
            {
                gameOver();
            }
            if (this.gameObject)
            {
                finalscore = Time.time * 10;
                score.text = (Time.time * 10).ToString("0");
            }
        }
        
    }
    IEnumerator getReady()
    {
        
        statusText.text = "GET READY!";
        yield return new WaitForSeconds(2f);
        statusText.text = "";
    }
    void gameOver()
    {
        if ((int)finalscore > PlayerPrefs.GetInt("R_highScore", 0))
        {
            PlayerPrefs.SetInt("R_highScore", (int)finalscore);
            highscore.text = "HIGHSCORE : " + finalscore.ToString("0");
        }
        statusText.text = "GAME OVER!";
        Destroy(gameObject);
    }
}
