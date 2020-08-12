using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text TotalCoins;
    public Text TotalCandy;
    private int totalCoin=0;
    private int totalCandy = 0;
    public Text coin;
    public Text candy;
    public Text score;
    public Text statusText;
    public Text highscore;
    private float finalscore;

    private void Start()
    {
        TotalCandy.text = "Total Candy : " + PlayerPrefs.GetInt("totalCandy", 0).ToString();
        TotalCoins.text = "Total Coins : " + PlayerPrefs.GetInt("totalCoins", 0).ToString();
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
        int candy = PlayerPrefs.GetInt("totalCandy", 0);
        PlayerPrefs.SetInt("totalCandy", candy + totalCandy);
        int coins = PlayerPrefs.GetInt("totalCoins", 0);
        PlayerPrefs.SetInt("totalCoins", coins + totalCoin);
        TotalCandy.text = "Total Candy : " + PlayerPrefs.GetInt("totalCandy", 0).ToString();
        TotalCoins.text = "Total Coins : " + PlayerPrefs.GetInt("totalCoins", 0).ToString();
        statusText.text = "GAME OVER!";
        Destroy(gameObject);
    }
}
