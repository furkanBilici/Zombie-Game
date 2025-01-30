using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 200f; // Fare hassasiyeti
    public Transform playerBody; // Karakterin g�vdesi
    private float xRotation = 0f;
    

    void Start()
    {
        // �mleci kilitle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)&& Cursor.visible)
        {
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.I) && !Cursor.visible)
        {
            Cursor.visible = true;
        }
        // Fare hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Dikey eksende kameray� d�nd�r (S�n�rlar� belirle)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukar� ve a�a�� s�n�r

        // Kameray� d�nd�r
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakteri yatay eksende d�nd�r
        playerBody.Rotate(Vector3.up * mouseX);
      
    }
}
