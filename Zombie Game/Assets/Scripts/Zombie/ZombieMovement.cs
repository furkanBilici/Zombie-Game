using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Rigidbody body;
    public GameObject player;
    public float speed = 3f;
    public Vector3 SeekDistance;//zombinin alan�
    void FixedUpdate()
    {
        Chase();
    }

    void Chase()
    {
        Vector3 distance = (body.transform.position - player.transform.position).normalized;//oyuncu zombinin b�lgesine giriyor mu kontrol i�in
        if (player.transform.position.z < body.transform.position.z && (distance.z<=SeekDistance.z && distance.x <= SeekDistance.x))
        {
            Vector3 direction = (player.transform.position - transform.position).normalized; // zombinin oyuncuya do�ru olan y�n�
            Vector3 targetPosition = transform.position + direction * speed * Time.fixedDeltaTime;     // hedef pozisyon

            body.MovePosition(Vector3.Lerp(transform.position, targetPosition, 0.5f));    // Rigidbody ile yumu�ak hareket sa�la

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));  // Zombinin y�n�n� oyuncuya �evirme kodu
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

