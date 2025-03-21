using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDotEnabler : MonoBehaviour
{
    public GameObject killer;
    public GameObject aimdotRed;
    RaycastHit hit;
    public int rayCount;
    public float angleRange;
    public float distance;
    bool set;

    private void Update()
    {
        float halfAngle = angleRange / 2;
        set=false;
        for (int i = 0; i < rayCount; i++)
        {
            float angle=Mathf.Lerp(-halfAngle,halfAngle,(float)i/(rayCount-1));
            Vector3 direction=Quaternion.Euler(0,angle,0)*transform.forward;

            if (Physics.Raycast(transform.position, direction, out hit, distance))
            {
                if (hit.collider.CompareTag("zombie"))
                {
                    set = true;
                    if(killer.activeSelf)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
        aimdotRed.SetActive(set);
    }
}
