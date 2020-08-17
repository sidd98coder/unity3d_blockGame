using UnityEngine;
using System.Collections;
public class SWIPEmovements : MonoBehaviour
{
    //attached with player



    private int currentTileNumber = -3;
    private float speed = 8f;


    bool canSwipe = false;

    private int currentLane = 1;        //0=left, 1=middle, 2=right
    private float LANE_DISTANCE = 1.3f;

    public SpawnManager spawnManagerScript;     //Spawning variables
    private int lastPrefabIndex = 0;
    private int secondLastPrefabIndex = 2;

    private Transform currentTileTransform;

    bool isGrounded = true;                       //jumping variables
    public Transform groundCheck;
    public LayerMask layer;
    private float radius = 0.1f;
    private Rigidbody rb;
    private const float jumpForce = 10f;
    float gravity = -15f;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //StartCoroutine(Stopper());
    }

    private void Update()
    {
        if (gameObject)
        {
            
            
                if (!canSwipe)      //checking input when NOT ON SWIPE AREA
                {
                    if (SWIPE.swipeRight)
                    {

                        if (currentLane < 2)    //will not move furthur RIGHT
                        {
                            transform.Translate(LANE_DISTANCE, 0, 0);       //move RIGHT
                        }
                        MoveLane(true);
                    }
                    if(SWIPE.swipeLeft)
                    {

                        if (currentLane > 0)    //will not move further LEFT
                        {
                            transform.Translate(-LANE_DISTANCE, 0, 0);      //move LEFT
                        }
                        MoveLane(false);
                    }

                }
                else           //checking inputs ON SWIPE AREA
                {
                    if(SWIPE.swipeRight)
                    {
                        transform.Rotate(0, 90f, 0, Space.Self);
                        canSwipe = false;           //can swipe ONLY ONCE
                        playerPositioningOnLanes();
                    }
                    else if(SWIPE.swipeLeft)
                    {
                        transform.Rotate(0, -90f, 0, Space.Self);
                        canSwipe = false;           //can swipe ONLY ONCE
                        playerPositioningOnLanes();
                    }
                    
                    
                }

            
            transform.position += transform.forward * speed * Time.deltaTime;       //move FORWARD

            if (isGrounded && !canSwipe && SWIPE.swipeUp)
            {

                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);   //giving force in upward direction
                isGrounded = false;
            }
            if (!isGrounded)
            {

                rb.AddForce(new Vector3(0, gravity, 0));       //giving downward movement when not grounded
            }
            if (Time.timeSinceLevelLoad % 60 == 0)  //increasing SPEED by 15% after 60 seconds !!
            {
                speed += 0.15f * speed;
                print("SPEED INCREASED!!!!!!!!!!!!");
            }
        }

    }


    void MoveLane(bool goingRight)              //checking Current Lane Number
    {
        if (goingRight)
        {
            currentLane++;
            if (currentLane == 3)
            {
                currentLane = 2;            //restricting lane
            }
        }
        else
        {
            currentLane--;
            if (currentLane == -1)
            {
                currentLane = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameObject)
        {
            //checking ground check !
            isGrounded = Physics.CheckSphere(groundCheck.position, radius * 0.9f, layer);

        }
    }

    void playerPositioningOnLanes()
    {
        //positioning player on lanes centre after rotation
        if (currentTileNumber >= 0)
        {

            int tileLength = spawnManagerScript.Tile.Count;
            currentTileTransform = spawnManagerScript.Tile[tileLength - 4];
            float ZlaneOffset = currentTileTransform.position.z - transform.position.z;
            float XlaneOffset = currentTileTransform.position.x - transform.position.x;
            float laneOffset = 0;
            Vector3 pos = transform.position;
            //calculating which offset should be used for deciding lane 
            if ((currentTileTransform.rotation.eulerAngles.y == 90.0f) || (currentTileTransform.rotation.eulerAngles.y == -90f))
            {
                laneOffset = ZlaneOffset;
            }
            else//((currentTileTransform.rotation.y == 0f) || (currentTileTransform.rotation.y == 180f))
            {
                laneOffset = XlaneOffset;
            }


            if (laneOffset >= 0.65 && laneOffset < 2.26f)                   //putting in lane 0
            {
                if (laneOffset == ZlaneOffset)
                    pos.z = currentTileTransform.position.z - 1.30f;
                else if (laneOffset == XlaneOffset)
                    pos.x = currentTileTransform.position.x - 1.30f;
                transform.position = pos;
                //future lane w.r.t. tile rotation on world axis
                if ((currentTileTransform.rotation.eulerAngles.y != 180f) && (currentTileTransform.rotation.eulerAngles.y != 90f))
                {
                    currentLane = 0;
                }
                else
                    currentLane = 2;


            }
            else if (laneOffset > -2.26f && laneOffset <= -0.65f)           //putting in lane 2
            {
                if (laneOffset == ZlaneOffset)
                    pos.z = currentTileTransform.position.z + 1.30f;
                else if (laneOffset == XlaneOffset)
                    pos.x = currentTileTransform.position.x + 1.30f;
                transform.position = pos;
                if ((currentTileTransform.rotation.eulerAngles.y != 180f) && (currentTileTransform.rotation.eulerAngles.y != 90f))
                {
                    currentLane = 2;
                }
                else
                    currentLane = 0;

            }
            else if (laneOffset > -0.65f && laneOffset < 0.65)              //putting in lane 1
            {
                if (laneOffset == ZlaneOffset)
                    pos.z = currentTileTransform.position.z;
                else if (laneOffset == XlaneOffset)
                    pos.x = currentTileTransform.position.x;
                transform.position = pos;
                currentLane = 1;
            }
        }
    }


    private void OnTriggerEnter(Collider other)             //Checking can Swipe or not!
    {
        if (other.gameObject.tag == "swipeCheck")
        {
            canSwipe = true;
        }
        if (other.gameObject.tag == "halfwayDoor")          //tile generator place
        {

            ++currentTileNumber;


            int randomIndex = lastPrefabIndex;//secondLastPrefabIndex;
            while ((randomIndex == lastPrefabIndex) || (randomIndex == secondLastPrefabIndex))      //generating random number(excluding previous)
            {
                randomIndex = Random.Range(0, 3);
            }
            secondLastPrefabIndex = lastPrefabIndex;
            lastPrefabIndex = randomIndex;
            spawnManagerScript.spawnTile(randomIndex);      //CALLING spawnTile() function when trigger with halfwayDoor(tile entrance)



            if (currentTileNumber > 2)      //start deleting tile when reaches(touches halfwayDoor) tile index 3 
            {
                spawnManagerScript.deleteTile();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "swipeCheck")
        {
            canSwipe = false;       //turning off swapping when exit swapping area
        }
    }

    IEnumerator Stopper()
    {
        print("stopper coroutine start");
        yield return new WaitForSeconds(15f);
    }

}
