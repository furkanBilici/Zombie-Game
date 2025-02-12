using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovementControlls : MonoBehaviour
{
    public Controller controller;
    public RectTransform gunImage; // Canvas'taki silah�n RectTransform'u
    public float swayAmountX = 10f; // Yatay hareket mesafesi (sa�a-sola)
    public float swayAmountY = 5f;  // Dikey hareket mesafesi (a�a��-yukar�)
    public float swaySpeed = 5f;    // Hareketin h�z�
    private Vector3 originalPosition;

    private float currentRotationAngle;
    public float rotationAmount;
    public float maxRotationAngle;

    void Start()
    {
        originalPosition = gunImage.anchoredPosition; // Silah�n orijinal pozisyonunu kaydet
    }

    void Update()
    {
        HandleMovementInput();
        if (!controller.stopPlayer)
        {
            RunningMovement();
            JumpMovement();
            CrouchMovement();
        }
        else
        {
            ResetGunPosition();
        }
    }

    void RunningMovement()
    {
        if (controller.moveSpeed == controller.speed)
        {
            // Yatayda Sin, dikeyde Cos kullanarak yar�m elips hareketi olu�turuyoruz
            float swayX = Mathf.Sin(Time.time * swaySpeed) * swayAmountX;
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * swayAmountY;
            gunImage.anchoredPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }
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

    void JumpMovement()
    {
        if (!controller.isGrounded)
        {
            // Z�plarken yukar�-a�a�� Cos hareketi
            float jumpSway = Mathf.Cos(Time.time * swaySpeed) * swayAmountY * 2;
            gunImage.anchoredPosition = originalPosition + new Vector3(0, jumpSway, 0);
        }
    }

    void CrouchMovement()
    {
        if (controller.speed == controller.crouchSpeed)
        {
            // Daha yava� yar�m elips hareket
            float swayX = Mathf.Sin(Time.time * (swaySpeed * 0.8f)) * (swayAmountX * 0.8f);
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * (swayAmountY * 0.8f);
            gunImage.anchoredPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }

    void ResetGunPosition()
    {
        gunImage.anchoredPosition = originalPosition; // Silah� orijinal pozisyonuna d�nd�r
    }
}
