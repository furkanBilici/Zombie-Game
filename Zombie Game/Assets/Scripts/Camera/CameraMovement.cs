using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Controller controller;
    public float swayAmountX = 0.1f; // Kameranýn saða-sola hareket mesafesi
    public float swayAmountY = 0.05f; // Kameranýn yukarý-aþaðý hareket mesafesi
    public float swaySpeed = 3f;     // Hareketin hýzý
    public float rotationAmount = 5f; // Kameranýn saða-sola dönme miktarý
    public float maxRotationAngle = 30f; // Kameranýn döneceði maksimum açý
    private Vector3 originalPosition;
    private float currentRotationAngle = 0f; // Anlýk dönüþ miktarý

    void Start()
    {
        originalPosition = transform.localPosition; // Kameranýn orijinal pozisyonunu kaydet
    }

    void Update()
    {
        if (!controller.stopPlayer)
        {
            RunningMovement();
            JumpMovement();
            CrouchMovement();
            HandleMovementInput();  // Yeni eklenen saða-sola kayma fonksiyonu
        }
        else
        {
            ResetCameraPosition();
        }
    }

    // Kameranýn saða veya sola dönmesini saðlayan fonksiyon
    void HandleMovementInput()
    {
        // A tuþuna basýldýðýnda sola dönme
        if (Input.GetKey(KeyCode.D))
        {
            if (currentRotationAngle > -maxRotationAngle) // Maksimum sola dönüþ mesafesi
            {
                currentRotationAngle -= rotationAmount * Time.deltaTime; // Yavaþça sola dön
            }
        }
        // D tuþuna basýldýðýnda saða dönme
        else if (Input.GetKey(KeyCode.A))
        {
            if (currentRotationAngle < maxRotationAngle) // Maksimum saða dönüþ mesafesi
            {
                currentRotationAngle += rotationAmount * Time.deltaTime; // Yavaþça saða dön
            }
        }
        else
        {
            // Tuþ býrakýldýðýnda kamerayý orijinal pozisyona döndür
            currentRotationAngle = Mathf.Lerp(currentRotationAngle, 0f, 0.03f); // Yavaþça geri dön
        }

        // Kameranýn Z rotasýný uygula
        transform.localRotation = Quaternion.Euler(0f, 0f, currentRotationAngle);
    }



    void RunningMovement()
    {
        if (controller.moveSpeed == controller.speed)
        {
            // Yatayda Sin, dikeyde Cos kullanarak yarým elips hareketi
            float swayX = Mathf.Sin(Time.time * swaySpeed) * swayAmountX;
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * swayAmountY;
            transform.localPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }

    void JumpMovement()
    {
        if (!controller.isGrounded)
        {
            // Zýplarken yukarý-aþaðý hareket (Cos ile yukarý-aþaðý sarsýntý efekti)
            float jumpSwayY = Mathf.Cos(Time.time * swaySpeed) * (swayAmountY * 2f);
            transform.localPosition = originalPosition + new Vector3(0, jumpSwayY, 0);
        }
    }

    void CrouchMovement()
    {
        if (controller.speed == controller.crouchSpeed)
        {
            // Daha yavaþ ve küçük elips hareket (eðilme sýrasýnda)
            float swayX = Mathf.Sin(Time.time * (swaySpeed * 0.5f)) * (swayAmountX * 0.5f);
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * (swayAmountY * 0.5f);
            transform.localPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }

    void ResetCameraPosition()
    {
        transform.localPosition = originalPosition; // Kamerayý orijinal pozisyonuna döndür
    }
}
