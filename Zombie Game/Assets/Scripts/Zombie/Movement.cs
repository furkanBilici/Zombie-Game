using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody body;
    public GameObject player;
    public float distanceX;
    public float distanceZ; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Chase()
    {
        distanceX=transform.position.x-player.transform.position.x;
        distanceZ=transform.position.z-player.transform.position.z;
        if (transform.position != player.transform.position)
        {
            if (distanceX > 0)
            {
                if (distanceZ > 0)
                {
                    transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
                }

            }
        }
    }
}
