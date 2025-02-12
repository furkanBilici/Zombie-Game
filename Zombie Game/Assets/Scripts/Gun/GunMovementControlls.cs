using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovementControlls : MonoBehaviour
{
    public Controller controller;
    public RectTransform gunImage; // Canvas'taki silahýn RectTransform'u
    public float swayAmountX = 10f; // Yatay hareket mesafesi (saða-sola)
    public float swayAmountY = 5f;  // Dikey hareket mesafesi (aþaðý-yukarý)
    public float swaySpeed = 5f;    // Hareketin hýzý
    private Vector3 originalPosition;

    private float currentRotationAngle;
    public float rotationAmount;
    public float maxRotationAngle;

    void Start()
    {
        originalPosition = gunImage.anchoredPosition; // Silahýn orijinal pozisyonunu kaydet
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
            // Yatayda Sin, dikeyde Cos kullanarak yarým elips hareketi oluþturuyoruz
            float swayX = Mathf.Sin(Time.time * swaySpeed) * swayAmountX;
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * swayAmountY;
            gunImage.anchoredPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }
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

    void JumpMovement()
    {
        if (!controller.isGrounded)
        {
            // Zýplarken yukarý-aþaðý Cos hareketi
            float jumpSway = Mathf.Cos(Time.time * swaySpeed) * swayAmountY * 2;
            gunImage.anchoredPosition = originalPosition + new Vector3(0, jumpSway, 0);
        }
    }

    void CrouchMovement()
    {
        if (controller.speed == controller.crouchSpeed)
        {
            // Daha yavaþ yarým elips hareket
            float swayX = Mathf.Sin(Time.time * (swaySpeed * 0.8f)) * (swayAmountX * 0.8f);
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * (swayAmountY * 0.8f);
            gunImage.anchoredPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }

    void ResetGunPosition()
    {
        gunImage.anchoredPosition = originalPosition; // Silahý orijinal pozisyonuna döndür
    }
}
