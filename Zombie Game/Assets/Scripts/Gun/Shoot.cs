using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject killerArea;
    public bool isGunCold;
    // Start is called before the first frame update
    void Start()
    {
        isGunCold = true;
    }

    // Update is called once per frame
    void Update()
    {
        ShootGun();
    }
    void ShootGun()
    {
        if (Input.GetKeyUp(KeyCode.F) && isGunCold)
        {
            killerArea.SetActive(true);
            Invoke("KillerAreaOff",0.1f);
            Invoke("WaitForGunGetCold", 1f);
            isGunCold=false;
        }
    }
    void KillerAreaOff()
    {
        killerArea.SetActive(false);
    }
    void WaitForGunGetCold()
    {
        isGunCold=true;
    }
}
