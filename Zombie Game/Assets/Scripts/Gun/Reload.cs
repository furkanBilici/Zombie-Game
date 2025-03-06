using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public Bullet bulletInfo;
    public int bulletAdd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            ReloadGun();
        
    }
    void ReloadGun() 
    {
        bulletAdd=10-bulletInfo.bulletAmount;
        if (bulletInfo.bulletAmount < 10 && Input.GetKeyDown(KeyCode.R) && bulletInfo.bulletTotal>0)
        {
            if (bulletAdd < bulletInfo.bulletTotal)
            {
                bulletInfo.bulletTotal -= bulletAdd;
                bulletInfo.bulletAmount += bulletAdd;
            }
            else
            {
                bulletInfo.bulletAmount += bulletInfo.bulletTotal;
                bulletInfo.bulletTotal = 0;
            }
        }
        
    }
}
