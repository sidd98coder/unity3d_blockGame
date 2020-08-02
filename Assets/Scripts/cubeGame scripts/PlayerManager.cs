using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform Player;
    public Text healthText;
    private float maxHealth =100f;
    public Text pointsText;
    private int points =0;
    public Text scoreText;
    public Text GameOverText;
    public Text levelClearText;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "100";
        pointsText.text= "0";
        pointsText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Player.position.z.ToString("0");
        if(Player.position.x>51f || Player.position.x < -51f)
        {
            gameOver();
        }
    }

    void gameOver()
    {
        Destroy(gameObject);
        GameOverText.text = "GAME OVER !";
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag=="Obstacle"){
            maxHealth -= 10f;
            if(maxHealth<=0){
                Debug.Log("Player DIED, Game Over !!!");
                gameOver();



            }
        }
        healthText.text = maxHealth.ToString("0");
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag=="Points"){
            points += 10;
            pointsText.text = points.ToString();
            Destroy(col.gameObject);

        }
        if (col.gameObject.tag == "FinishLine")
        {
            
            Destroy(col.gameObject);
            Destroy(gameObject);
            levelClearText.text = "LEVEL CLEAR!";
            Debug.Log("Player Wins, Level Clear !!!");
        }
    }

}
