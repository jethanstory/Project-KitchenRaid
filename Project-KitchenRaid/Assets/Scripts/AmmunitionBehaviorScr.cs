using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBehaviorScr : MonoBehaviour
{
    public int bulletCount = 7;
    public GameObject sliderBack;
    public GameObject sliderOriginal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletCount != 0)
        {
            bulletCount--;
        }
        if (bulletCount == 0)
        {
            sliderBack.SetActive(true);
            sliderOriginal.SetActive(false);
            GameObject.Find("M1911 Handgun_Model_1").GetComponent<SimpleShoot>().enabled = false;
        }

    }
}
