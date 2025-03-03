using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int bulletAmount;
    public int bulletTotal;
    public Text bulletText; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = bulletAmount.ToString()+"/"+bulletTotal.ToString();  
    }
}
