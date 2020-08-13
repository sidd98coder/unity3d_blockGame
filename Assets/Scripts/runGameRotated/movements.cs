using System.Collections;

using UnityEngine;


public class movements : MonoBehaviour
{
    private int currentTileNumber = -3;
    private float speed = 8f;
    private int check = 0;
        
    bool canSwipe = false;
    
    private int currentLane = 1;        //0=left, 1=middle, 2=right
    private float LANE_DISTANCE = 1.3f;
    
    
    public SpawnManager spawnManagerScript;
    private int lastPrefabIndex = 0;
    private int secondLastPrefabIndex = 2;

    private Transform currentTileTransform;

    bool isGrounded=true;
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
        if (this.gameObject)
        {


            if (Input.GetMouseButtonUp(0))
            {
                if ((Input.mousePosition.x > Screen.width / 2) && !canSwipe)
                {

                    if (currentLane < 2)
                    {
                        transform.Translate(LANE_DISTANCE, 0, 0);
                    }
                    MoveLane(true);
                }
                else if ((Input.mousePosition.x <= Screen.width / 2) && !canSwipe)
                {

                    if (currentLane > 0)
                    {
                        transform.Translate(-LANE_DISTANCE, 0, 0);
                    }
                    MoveLane(false);
                }
            }
            if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1)) && isGrounded)
            {
                
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                
            }
            if (!isGrounded)
            {
                
                rb.AddForce(new Vector3(0, verticalVelocity, 0));
            }
            //transform.Translate(transform.forward * speed * Time.deltaTime, Space.Self);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        
    }


    void MoveLane(bool goingRight)              //checking Current Lane Number
    {
        if (goingRight)
        {
            currentLane++;
            if (currentLane == 3)
            {
                currentLane = 2;
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
            isGrounded = Physics.CheckSphere(groundCheck.position, radius, layer);
            

            if (Input.GetMouseButtonUp(0))
            {
                if ((Input.mousePosition.x > Screen.width / 2) && canSwipe)
                {
                    transform.Rotate(0, 90.0f, 0, Space.Self);
                }
                else if ((Input.mousePosition.x <= Screen.width / 2) && canSwipe)
                {
                    transform.Rotate(0, -90.0f, 0, Space.Self);
                }
                if (canSwipe && currentTileNumber>=0)
                {
                    int tileLength = spawnManagerScript.Tile.Count;
                    currentTileTransform = spawnManagerScript.Tile[tileLength - 3];
                    float ZlaneOffset = currentTileTransform.position.z - transform.position.z;
                    float XlaneOffset = currentTileTransform.position.x - transform.position.x;
                    float laneOffset = 0;
                    if (currentTileTransform.rotation.y == 90f || currentTileTransform.rotation.y == -90f)
                    {
                        laneOffset = ZlaneOffset;
                        print("ZlaneOffset");
                    }

                    else if (currentTileTransform.rotation.y == 0f || currentTileTransform.rotation.y == 180f)
                    {
                        print("XlaneOffset");
                        laneOffset = XlaneOffset;
                    }

                    Vector3 pos = transform.position;
                    if (laneOffset >= 0.65 && laneOffset < 2.26f)                   //putting in lane 0
                    {
                        if (laneOffset == ZlaneOffset)
                            pos.z = currentTileTransform.position.z - 1.3f;
                        else if (laneOffset == XlaneOffset)
                            pos.x = currentTileTransform.position.x - 1.3f;
                        transform.position = pos;
                        if((currentTileTransform.rotation.y != 180f) && (currentTileTransform.rotation.y != 90f))
                        {
                            currentLane = 0;
                        }else
                            currentLane = 2;


                    }
                    else if (laneOffset > -2.26f && laneOffset <= -0.65f)           //putting in lane 2
                    {
                        if (laneOffset == ZlaneOffset)
                            pos.z = currentTileTransform.position.z + 1.3f;
                        else if (laneOffset == XlaneOffset)
                            pos.x = currentTileTransform.position.x + 1.3f;
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
                    print("check : " + check);
                    check++;
                    print("laneOffset : " + laneOffset);
                } 

                //print(transform.position);
                canSwipe = false;                                               //can swipe only once

                
                
                
            }
        }
    }


    /*private void LateUpdate()
    {
        if (currentTileNumber >= 0)
        {
            int tileLength = spawnManagerScript.Tile.Count;
            currentTileTransform = spawnManagerScript.Tile[tileLength - 3];
            float ZlaneOffset = currentTileTransform.position.z - transform.position.z;
            float XlaneOffset = currentTileTransform.position.x - transform.position.x;
            float laneOffset = 0;
            if (currentTileTransform.rotation.y == 90f || currentTileTransform.rotation.y == -90f)
            {

                laneOffset = ZlaneOffset;
            }

            else if (currentTileTransform.rotation.y == 0f || currentTileTransform.rotation.y == 180f)
            {

                laneOffset = XlaneOffset;
            }

            Vector3 pos = transform.position;
            if (laneOffset >= 0.65 && laneOffset < 2.26f)                   //putting in lane 0
            {
                if (laneOffset == ZlaneOffset)
                    pos.z = currentTileTransform.position.z - 1.3f;
                else if (laneOffset == XlaneOffset)
                    pos.x = currentTileTransform.position.x - 1.3f;
                transform.position = pos;
                if(currentTileTransform.rotation.y != 180)
                {
                    currentLane = 0;
                }else
                    currentLane = 2;
                //currentLane = 0;
                /*if ((currentTileTransform.rotation.y != 180f) && (currentTileTransform.rotation.y != 90f))
                {
                    currentLane = 0;
                }
                else
                    currentLane = 2;*/


            /*}
            else if (laneOffset > -2.26f && laneOffset <= -0.65f)           //putting in lane 2
            {
                if (laneOffset == ZlaneOffset)
                    pos.z = currentTileTransform.position.z + 1.3f;
                else if (laneOffset == XlaneOffset)
                    pos.x = currentTileTransform.position.x + 1.3f;
                transform.position = pos;
                if (currentTileTransform.rotation.y != 180)
                {
                    currentLane = 2;
                }else
                    currentLane = 0;
                //currentLane = 2;
                /*if ((currentTileTransform.rotation.y != 180f) && (currentTileTransform.rotation.y != 90f))
                {
                    currentLane = 2;
                }
                else
                    currentLane = 0;*/

            /*}
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
        

    }*/
    private void OnTriggerEnter(Collider other)             //Checking can Swipe or not!
    {
        if (other.gameObject.tag == "swipeCheck")
        {
            canSwipe = true;
        }
        if (other.gameObject.tag == "halfwayDoor")
        {
            ++currentTileNumber;
            //print("Player is on " + currentTileNumber);

                int randomIndex = lastPrefabIndex;//secondLastPrefabIndex;
                while (randomIndex == lastPrefabIndex)//secondLastPrefabIndex)
                {
                    randomIndex = Random.Range(0, 3);
                }
                //secondLastPrefabIndex = lastPrefabIndex;
                lastPrefabIndex = randomIndex;
                spawnManagerScript.spawnTile(randomIndex);
            


            if (currentTileNumber > 1)
            {
                spawnManagerScript.deleteTile();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "swipeCheck")
        {
            canSwipe = false;
        }
    }
    /*private void OnCollisionEnter(Collision collision)      ////Generating random tile on collision with next tile
    {
        
        if (collision.gameObject.tag == "Tile")
        {
            

            int randomIndex = lastPrefabIndex;//secondLastPrefabIndex;
            while (randomIndex == lastPrefabIndex)//secondLastPrefabIndex)
            {
                randomIndex = Random.Range(0, 3);
            }
            //secondLastPrefabIndex = lastPrefabIndex;
            lastPrefabIndex = randomIndex;
            spawnManagerScript.spawnTile(randomIndex);
        }
        
    }*/
    IEnumerator Stopper()
    {
        print("stopper coroutine start");
        yield return new WaitForSeconds(15f);
    }
}