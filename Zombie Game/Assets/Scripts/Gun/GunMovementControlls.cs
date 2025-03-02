using System.Collections;
using UnityEngine;

public class GunMovementControlls : MonoBehaviour
{
    public Shoot Shoot;
    public RectTransform flame;
    public Controller controller;
    public RectTransform gunImage;

    public float swayAmountX = 10f;
    public float swayAmountY = 5f;
    public float swaySpeed = 5f;
    private Vector3 originalPosition;
    private Vector3 originalFlamePosition;

    private float currentRotationAngle;
    public float rotationAmount;
    public float maxRotationAngle;

    public float recoilAmount = 30f;
    public float recoilSpeed = 0.1f;
    private bool isRecoiling = false;

    void Start()
    {
        originalPosition = gunImage.anchoredPosition;
        originalFlamePosition = flame.anchoredPosition;
    }

    void Update()
    {
        HandleMovementInput();

        if (!controller.stopPlayer)
        {
            if (!isRecoiling)  // Eðer geri tepme oluyorsa sway çalýþmasýn!
            {
                RunningMovement();
                JumpMovement();
                CrouchMovement();
            }
        }
        else
        {
            ResetGunPosition();
        }

        ShotMovement();
    }

    void ShotMovement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isRecoiling && Shoot.isGunCold && Shoot.shooting)
        {
            StartCoroutine(RecoilEffect());
        }
    }

    IEnumerator RecoilEffect()
    {
        isRecoiling = true;

        Vector3 recoilPosition = originalPosition + new Vector3(-recoilAmount, 0, 0);
        Vector3 recoilFlamePosition = originalFlamePosition + new Vector3(-recoilAmount * 0.5f, 0, 0);

        float elapsedTime = 0f;
        while (elapsedTime < recoilSpeed)
        {
            gunImage.anchoredPosition = Vector3.Lerp(originalPosition, recoilPosition, elapsedTime / recoilSpeed);
            flame.anchoredPosition = Vector3.Lerp(originalFlamePosition, recoilFlamePosition, elapsedTime / recoilSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.05f); // Kýsa bir bekleme süresi

        elapsedTime = 0f;
        while (elapsedTime < recoilSpeed)
        {
            gunImage.anchoredPosition = Vector3.Lerp(recoilPosition, originalPosition, elapsedTime / recoilSpeed);
            flame.anchoredPosition = Vector3.Lerp(recoilFlamePosition, originalFlamePosition, elapsedTime / recoilSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gunImage.anchoredPosition = originalPosition;
        flame.anchoredPosition = originalFlamePosition;

        isRecoiling = false;
    }

    void RunningMovement()
    {
        if (controller.moveSpeed == controller.speed)
        {
            float swayX = Mathf.Sin(Time.time * swaySpeed) * swayAmountX;
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * swayAmountY;
            gunImage.anchoredPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (currentRotationAngle > -maxRotationAngle)
            {
                currentRotationAngle -= rotationAmount * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (currentRotationAngle < maxRotationAngle)
            {
                currentRotationAngle += rotationAmount * Time.deltaTime;
            }
        }
        else
        {
            currentRotationAngle = Mathf.Lerp(currentRotationAngle, 0f, 0.03f);
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, currentRotationAngle);
    }

    void JumpMovement()
    {
        if (!controller.isGrounded)
        {
            float jumpSway = Mathf.Cos(Time.time * swaySpeed) * swayAmountY * 2;
            gunImage.anchoredPosition = originalPosition + new Vector3(0, jumpSway, 0);
        }
    }

    void CrouchMovement()
    {
        if (controller.speed == controller.crouchSpeed)
        {
            float swayX = Mathf.Sin(Time.time * (swaySpeed * 0.8f)) * (swayAmountX * 0.8f);
            float swayY = Mathf.Cos(Time.time * (swaySpeed * 0.5f)) * (swayAmountY * 0.8f);
            gunImage.anchoredPosition = originalPosition + new Vector3(swayX, swayY, 0);
        }
    }

    void ResetGunPosition()
    {
        gunImage.anchoredPosition = originalPosition;
    }
}
