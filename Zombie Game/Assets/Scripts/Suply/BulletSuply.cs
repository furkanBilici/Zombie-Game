using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSuply : MonoBehaviour
{
    public Bullet bulletInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="player" && other.gameObject.tag != "killer")
        {
            bulletInfo.bulletTotal += 10;
            Destroy(this.gameObject);
            
        }
    }
}
