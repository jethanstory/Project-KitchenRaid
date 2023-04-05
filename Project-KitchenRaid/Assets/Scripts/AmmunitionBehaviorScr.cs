using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionBehaviorScr : MonoBehaviour
{
    public int bulletCount = 7;
    public int bulletNeeded = 0;
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
        bulletNeeded = 7 - bulletCount;

    }

    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if(other.gameObject.tag == "Ammo") //on the object you want to pick up set the tag to be anything, in this case "object"
        {
           bulletCount+=bulletNeeded;
           sliderBack.SetActive(false);
           sliderOriginal.SetActive(true);
           GameObject.Find("M1911 Handgun_Model_1").GetComponent<SimpleShoot>().enabled = true;
        }
    }
}
