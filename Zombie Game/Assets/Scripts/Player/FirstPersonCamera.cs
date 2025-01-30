using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 200f; // Fare hassasiyeti
    public Transform playerBody; // Karakterin gövdesi
    private float xRotation = 0f;
    

    void Start()
    {
        // Ýmleci kilitle
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

        // Dikey eksende kamerayý döndür (Sýnýrlarý belirle)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukarý ve aþaðý sýnýr

        // Kamerayý döndür
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakteri yatay eksende döndür
        playerBody.Rotate(Vector3.up * mouseX);
      
    }
}
