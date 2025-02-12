using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Controller controller;
    public float swayAmountX = 0.1f; // Kameran�n sa�a-sola hareket mesafesi
    public float swayAmountY = 0.05f; // Kameran�n yukar�-a�a�� hareket mesafesi
    public float swaySpeed = 3f;     // Hareketin h�z�
    public float rotationAmount = 5f; // Kameran�n sa�a-sola d�nme miktar�
    public float maxRotationAngle = 30f; // Kameran�n d�nece�i maksimum a��
    private Vector3 originalPosition;
    private float currentRotationAngle = 0f; // Anl�k d�n�� miktar�

    void Start()
    {
        originalPosition = transform.localPosition; // Kameran�n orijinal pozisyonunu kaydet
    }

    void Update()
    {
        if (!controller.stopPlayer)
        {
            RunningMovement();
            JumpMovement();
            CrouchMovement();
            HandleMovementInput();  // Yeni eklenen sa�a-sola kayma fonksiyonu
        }
        else
        {
            ResetCameraPosition();
        }
    }

    // Kameran�n sa�a veya sola d�nmesini sa�layan fonksiyon
    void HandleMovementInput()
    {
        // A tu�una bas�ld���nda sola d�nme
        if (Input.GetKey(KeyCode.D))
        {
            if (currentRotationAngle > -maxRotationAngle) // Maksimum sola d�n�� mesafesi
            {
                currentRotationAngle -= rotationAmount * Time.deltaTime; // Yava��a sola d�n
            }
        }
        // D tu�una bas�ld���nda sa�a d�nme
        else if (Input.GetKey(KeyCode.A))
        {
            if (currentRotationAngle < maxRotationAngle) // Maksimum sa�a d�n�� mesafesi
            {
                currentRotationAngle += rotationAmount * Time.deltaTime; // Yava��a sa�a d�n
            }
        }
        else
        {
            // Tu� b�rak�ld���nda kameray� orijinal pozisyona d�nd�r
            currentRotationAngle = Mathf.Lerp(currentRotationAngle, 0f, 0.03f); // Yava��a geri d�n
        }

        // Kameran�n Z rotas�n� uygula
        transform.localRotation = Quaternion.Euler(0f, 0f, currentRotationAngle);
    }



    void RunningMovement()
    {
        if (controller.moveSpeed == controller.speed)
        {
            // Yatayda Sin, dikeyde Cos kullanarak yar�m elips hareketi
            float swayX = Mathf.Sin(Time.time * swaySpeed) * swayAmountX;
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * swayAmountY;
            transform.localPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }

    void JumpMovement()
    {
        if (!controller.isGrounded)
        {
            // Z�plarken yukar�-a�a�� hareket (Cos ile yukar�-a�a�� sars�nt� efekti)
            float jumpSwayY = Mathf.Cos(Time.time * swaySpeed) * (swayAmountY * 2f);
            transform.localPosition = originalPosition + new Vector3(0, jumpSwayY, 0);
        }
    }

    void CrouchMovement()
    {
        if (controller.speed == controller.crouchSpeed)
        {
            // Daha yava� ve k���k elips hareket (e�ilme s�ras�nda)
            float swayX = Mathf.Sin(Time.time * (swaySpeed * 0.5f)) * (swayAmountX * 0.5f);
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * (swayAmountY * 0.5f);
            transform.localPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }

    void ResetCameraPosition()
    {
        transform.localPosition = originalPosition; // Kameray� orijinal pozisyonuna d�nd�r
    }
}
