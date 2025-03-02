using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject killerArea;
    public bool isGunCold;
    public float gunColdTime=0.2f;
    public Bullet bulletInfo;
    public bool shooting;

    public GameObject flame;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;

    // Start is called before the first frame update
    void Start()
    {
        shooting = false;
        isGunCold = true;
    }

    // Update is called once per frame
    void Update()
    {
        ShootGun();
    }
    void ShootGun()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && isGunCold)
        {
            if (bulletInfo.bulletAmount > 0)
            {
                shooting = true;
                bulletInfo.bulletAmount -= 1;
                killerArea.SetActive(true);           
                Invoke("KillerAreaOff",0.1f);
                Invoke("WaitForGunGetCold", gunColdTime); 
                flame.SetActive(true);
                isGunCold=false; StartCoroutine(ActivateObjects());
            }
            
            
        }
    }
    IEnumerator ActivateObjects()
    {
        yield return new WaitForSeconds(0.02f);
        one.SetActive(false);

        yield return new WaitForSeconds(0.02f);
        two.SetActive(false);

        yield return new WaitForSeconds(0.02f);
        three.SetActive(false);

        yield return new WaitForSeconds(0.02f);
        four.SetActive(false); // 4. obje kapanýyor

        yield return new WaitForSeconds(0.01f);
        flame.SetActive(false); // Flame kapanýyor

        // 4 obje tekrar açýlýyor
        one.SetActive(true);
        two.SetActive(true);
        three.SetActive(true);
        four.SetActive(true);
    }

    void KillerAreaOff()
    {
        shooting=false; 
        killerArea.SetActive(false);
    }
    void WaitForGunGetCold()
    {
        isGunCold=true;
    }

}
