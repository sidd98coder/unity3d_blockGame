using System.Collections;

using UnityEngine;
using UnityEngine.Tilemaps;

public class movements : MonoBehaviour
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

    bool isGrounded=true;                       //jumping variables
    public Transform groundCheck;
    public LayerMask layer;
    private float radius = 0.25f;
    private Rigidbody rb;
    private const float jumpForce = 2f;
    float verticalVelocity = -15f;

    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //StartCoroutine(Stopper());
    }

    private void Update()
    {
        if (gameObject)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!canSwipe)      //checking input when NOT ON SWIPE AREA
                {
                    if (Input.mousePosition.x > Screen.width / 2)
                    {

                        if (currentLane < 2)    //will not move furthur RIGHT
                        {
                            transform.Translate(LANE_DISTANCE, 0, 0);       //move RIGHT
                        }
                        MoveLane(true);
                    }
                    else
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
                    if (Input.mousePosition.x > Screen.width / 2)
                    {
                        transform.Rotate(0, 90f, 0, Space.Self);
                        //print("before positioning : " + transform.position);
                    }
                    else
                    {
                        transform.Rotate(0, -90f, 0, Space.Self);
                        //print("before positioning : " + transform.position);
                    }
                    playerPositioningOnLanes();
                    canSwipe = false;           //can swipe ONLY ONCE
                }
                
            }
            
            transform.position += transform.forward * speed * Time.deltaTime;       //move FORWARD
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
            isGrounded = Physics.CheckSphere(groundCheck.position, radius, layer);
            if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1)) && isGrounded && !canSwipe)
            {

                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);   //giving force in upward direction

            }
            if (!isGrounded)
            {

                rb.AddForce(new Vector3(0, verticalVelocity, 0));       //giving downward movement when not grounded
            }
        }
    }

    void playerPositioningOnLanes()
    {
        //positioning player on lanes centre after rotation
        if (currentTileNumber >= 0)
        {
            int tileLength = spawnManagerScript.Tile.Count;
            currentTileTransform = spawnManagerScript.Tile[tileLength - 3];
            float ZlaneOffset = currentTileTransform.position.z - transform.position.z;
            float XlaneOffset = currentTileTransform.position.x - transform.position.x;
            float laneOffset;
            Vector3 pos = transform.position;
            //calculating which offset should be used for deciding lane 
            if ((currentTileTransform.rotation.y == 90f) || (currentTileTransform.rotation.y == -90f))
            {
                //print("ZlaneOffset");
                laneOffset = ZlaneOffset;
                
            }
            else//((currentTileTransform.rotation.y == 0f) || (currentTileTransform.rotation.y == 180f))
            {
                //print("XlaneOffset");
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
                if ((currentTileTransform.rotation.y != 180f) && (currentTileTransform.rotation.y != 90f))
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
                if ((currentTileTransform.rotation.y != 180f) && (currentTileTransform.rotation.y != 90f))
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

            /*print("laneOffset : " + laneOffset);
            print("after positioning : " + transform.position);
            print("tile position for comparision : " + currentTileTransform.position);
            print("tile rotation for comparision : " + currentTileTransform.rotation.y);
            print("tileLength - 3 = "+(tileLength-3));*/

            print("Tile Rotation on Swipe : " + currentTileTransform.rotation.y);
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
            print("Player is on " + currentTileNumber);

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