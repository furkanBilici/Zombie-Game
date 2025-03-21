using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillerArea : MonoBehaviour
{
    RaycastHit hit;
    public int rayCount;
    public float angleRange;
    public float distance;
    private void Update()
    {
        float halfAngle = angleRange / 2;
        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-halfAngle, halfAngle, (float)i / (rayCount - 1));
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.Raycast(transform.position, direction, out hit, distance))
            {
                if (hit.collider.CompareTag("zombie"))
                {
                   Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}



