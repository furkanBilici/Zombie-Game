using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed;
    public Rigidbody body;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float crouchSpeed = 2.5f;
    public Transform playerCamera;

    public bool isGrounded;
    private float originalHeight;
    private float crouchHeight;
    public bool stopPlayer;

    void Start()
    {
        stopPlayer = false;
        if (body == null)
            body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
        originalHeight = transform.localScale.y;
        crouchHeight = originalHeight*0.5f;
    }

    void Update()
    {
        if (!stopPlayer)
        {
            Move();
            Jump();
            Crouch();
        }
        StopPlayer();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = 1;

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection.y = 0;
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = moveSpeed;
        }

        body.MovePosition(body.position + moveDirection * speed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, originalHeight, transform.localScale.z);
        }
    }
    void StopPlayer()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (stopPlayer == true) 
            { stopPlayer = false; }
            else if (stopPlayer == false)
            { stopPlayer = true; }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
  

}
