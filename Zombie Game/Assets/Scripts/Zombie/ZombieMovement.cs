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
    public Vector3 SeekDistance;//zombinin alan�

    Vector3 direction;  // Y�n� normalize et
    Vector3 newPosition;   // Sabit h�zla ilerle
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
            direction = (player.transform.position - body.position).normalized;  // Y�n� normalize et
            newPosition = body.position + direction * speed * Time.deltaTime;   // Sabit h�zla ilerle
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
        if (body.position.z < player.transform.position.z )  // Oyuncunun arkas�nda kalma kontrol�
        {
            Destroy(gameObject);
        }
    }
}

