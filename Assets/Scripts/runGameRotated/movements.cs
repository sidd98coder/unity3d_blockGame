using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movements : MonoBehaviour
{
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }
}
