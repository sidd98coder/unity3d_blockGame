using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movements : MonoBehaviour
{
    private float speed = 7f;
    private CharacterController controller;
    bool canSwipe = false;
    public float xDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //transform.position += transform.TransformDirection(Vector3.forward*speed*Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)               //Mouse Controls
            {
                if (!canSwipe)
                {
                    transform.Translate(Vector3.right * xDistance*Time.deltaTime);                 //move 1.4 units ?????? 1.4 is lane length
                }
                else
                {
                    transform.Rotate(0, 90, 0, Space.Self);
                }
            }
            else
            {
                if (!canSwipe)
                {
                    transform.Translate(Vector3.left * xDistance * Time.deltaTime);                //move 1.4 units ??????
                }
                else
                {
                    transform.Rotate(0, -90, 0, Space.Self);
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)             //Checking can Swipe or not!
    {
        if (other.gameObject.tag == "swipeCheck")
        {
            canSwipe = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "swipeCheck")
        {
            canSwipe = false;
        }
    }
}
