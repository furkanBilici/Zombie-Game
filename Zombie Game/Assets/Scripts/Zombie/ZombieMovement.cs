using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Rigidbody body;
    public GameObject player;
    public float speed = 3f;
    float floater = Time.deltaTime;
    public Vector3 SeekDistance;//zombinin alaný
    void Update()
    {
        Chase();
        CheckIfBehindPlayer();
    }

    void Chase()
    {
        Vector3 distance = new Vector3(Mathf.Abs(body.transform.position.x - player.transform.position.x), 0, Mathf.Abs(body.transform.position.z - player.transform.position.z));

        if (distance.z <= SeekDistance.z && distance.x <= SeekDistance.x)
        {
            Vector3 direction = (player.transform.position - body.position).normalized;  // Yönü normalize et
            Vector3 newPosition = body.position + direction * speed * Time.deltaTime;   // Sabit hýzla ilerle
            body.MovePosition(newPosition);

            Vector3 lookDirection = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(lookDirection);
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

