using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Rigidbody body;
    public GameObject player;
    public float speed = 3f;
    public Vector3 SeekDistance;//zombinin alaný
    void FixedUpdate()
    {
        Chase();
    }

    void Chase()
    {
        Vector3 distance = (body.transform.position - player.transform.position).normalized;//oyuncu zombinin bölgesine giriyor mu kontrol için
        if (player.transform.position.z < body.transform.position.z && (distance.z<=SeekDistance.z && distance.x <= SeekDistance.x))
        {
            Vector3 direction = (player.transform.position - transform.position).normalized; // zombinin oyuncuya doðru olan yönü
            Vector3 targetPosition = transform.position + direction * speed * Time.fixedDeltaTime;     // hedef pozisyon

            body.MovePosition(Vector3.Lerp(transform.position, targetPosition, 0.5f));    // Rigidbody ile yumuþak hareket saðla

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));  // Zombinin yönünü oyuncuya çevirme kodu
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

