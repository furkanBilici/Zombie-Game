using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Rigidbody body;
    public GameObject player;
    public float speed = 3f;
    public Vector3 SeekDistance;//zombinin alaný

    Vector3 direction;  // Yönü normalize et
    Vector3 newPosition;   // Sabit hýzla ilerle
    Vector3 lookDirection;
    Vector3 distance;

    bool canChase;
    private void Start()
    {
        canChase = false;
    }
    void Update()
    {
        ChaseControll();
        CheckIfBehindPlayer();
    }
    private void FixedUpdate()
    {
        Chase();
    }
    void ChaseControll()
    {
        distance = new Vector3(Mathf.Abs(body.transform.position.x - player.transform.position.x), 0, Mathf.Abs(body.transform.position.z - player.transform.position.z));

        if (distance.z <= SeekDistance.z && distance.x <= SeekDistance.x)
        {
            canChase = true;
            direction = (player.transform.position - body.position).normalized;  // Yönü normalize et
            newPosition = body.position + direction * speed * Time.deltaTime;   // Sabit hýzla ilerle
            lookDirection = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z); 
            transform.LookAt(lookDirection);
        }
        else
        {
            canChase = false;
        }
    }
    void Chase()
    {
        if (canChase)
        {
            body.MovePosition(newPosition);
        }
    }
    void CheckIfBehindPlayer()
    {
        if (body.position.z < player.transform.position.z )  // Oyuncunun arkasýnda kalma kontrolü
        {
            Destroy(gameObject);
        }
    }
}

