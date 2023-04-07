using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualReloadPistolScr : MonoBehaviour
{
    public GameObject gunHand;
    public GameObject magHand;
    public GameObject bulletObject;

    public float speed = 2.0f;

    // The target (cylinder) position.
    public Transform gunTarget;
    public Transform magTarget;
    public Transform bulletTarget;
    public Transform pocketTarget;
    public Transform gunPocketTarget;
    public Transform magPocketTarget;
    public Transform bulletPocketTarget;
    public bool putAwayGun = false;
    public bool takeOutGun = false;
    public bool gunReset;

    public bool bulletOut = false;
    public bool bulletIn = false;

    public Transform bulletSpawnPoint;

    public float addBullets;

    public GameObject fpsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GunMovement();
        //StabKnife();

        if (Input.GetKeyDown(KeyCode.R) && fpsPlayer.GetComponent<AmmunitionBehaviorScr>().bulletNeeded > 0)
        //if (Input.GetKeyDown(KeyCode.V))
        {
            putAwayGun = true;
            bulletOut = true;
            GameObject.Find("M1911 Handgun_Model_1").GetComponent<SimpleShoot>().enabled = false;
            
                
        }

        // if (fpsPlayer.GetComponent<AmmunitionBehaviorScr>().bulletNeeded == 0)
        // //if (Input.GetKeyDown(KeyCode.V))
        // {
        //     takeOutGun = true;
                
        // }
    }

    public void GunMovement()
    {
        if (takeOutGun)
        {
            gunReset = false;
            var step =  speed * Time.deltaTime; // calculate distance to move
            gunHand.transform.position = Vector3.MoveTowards(gunHand.transform.position, gunTarget.position, step);
            magHand.transform.position = Vector3.MoveTowards(magHand.transform.position, magPocketTarget.position, step);
            

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(gunHand.transform.position, gunTarget.position) < 0.0001f)//< 0.001f)
            {
               Debug.Log("HIT");
               takeOutGun = false;
               //gunHand.transform.position = Vector3.MoveTowards(gunHand.transform.position, gunPocketTarget.position, step);
            }
            bulletObject.SetActive(false);
            // if (Vector3.Distance(magHand.transform.position, magTarget.position) < 0.0001f)//< 0.001f)
            // {
            // //    putAwayGun = false;
               
            // }
        }

        if (putAwayGun)
        {
            addBullets += Time.deltaTime;

            bulletObject.SetActive(true);
            
            // GameObject bulletInstance = Instantiate(bulletObject, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            BulletMove();
            fpsPlayer.GetComponent<AmmunitionBehaviorScr>().bulletNeeded -= addBullets;
            var step = speed * Time.deltaTime; // calculate distance to move
            gunHand.transform.position = Vector3.MoveTowards(gunHand.transform.position, gunPocketTarget.position, step);
            magHand.transform.position = Vector3.MoveTowards(magHand.transform.position, magTarget.position, step);
            // bulletInstance.transform.position = Vector3.MoveTowards(bulletInstance.transform.position, bulletPocketTarget.position, step);
            // //Destroy(bulletInstance);
            // bulletInstance.transform.position = Vector3.MoveTowards(bulletPocketTarget.transform.position, bulletSpawnPoint.position, 100);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(gunHand.transform.position, gunPocketTarget.position) < 0.0001f)//< 0.001f)
            {
               
               
            }

            if (fpsPlayer.GetComponent<AmmunitionBehaviorScr>().bulletNeeded <= 0)
            {
                fpsPlayer.GetComponent<AmmunitionBehaviorScr>().bulletNeeded = 0;
                takeOutGun = true;
                putAwayGun = false;
                gunReset = true;
                addBullets = 0;
                GameObject.Find("M1911 Handgun_Model_1").GetComponent<SimpleShoot>().enabled = true;
            }
            
            // if (Vector3.Distance(magHand.transform.position, magTarget.position) < 0.0001f)//< 0.001f)
            // {
            //    Debug.Log("HIT");
            //    //takeOutGun = false;
            //    //magHand.transform.position = Vector3.MoveTowards(magHand.transform.position, magPocketTarget.position, step);
            // }
        }
    }

    // public void MagMovement()
    // {
    //     if (takeOut)
    //     {
    //         var step =  speed * Time.deltaTime; // calculate distance to move
    //         magHand.transform.position = Vector3.MoveTowards(magHand.transform.position, magTarget.position, step);

    //         // Check if the position of the cube and sphere are approximately equal.
    //         if (Vector3.Distance(magHand.transform.position, magTarget.position) < 0.0001f)//< 0.001f)
    //         {
    //            Debug.Log("HIT");
    //            takeOutGun = false;
    //            //magHand.transform.position = Vector3.MoveTowards(magHand.transform.position, magPocketTarget.position, step);
    //         }
    //     }

    //     if (putAwayGun)
    //     {
    //         var step =  speed * Time.deltaTime; // calculate distance to move
    //         magHand.transform.position = Vector3.MoveTowards(magHand.transform.position, magPocketTarget.position, step);

    //         // Check if the position of the cube and sphere are approximately equal.
    //         if (Vector3.Distance(magHand.transform.position, magTarget.position) < 0.0001f)//< 0.001f)
    //         {
    //            putAwayGun = false;
               
    //         }
    //     }
    // }

    private void BulletMove()
    {
        //var step =  speed * Time.deltaTime; // calculate distance to move
        var step =  Time.deltaTime /2; // calculate distance to move

        if (bulletOut)
        {
            bulletObject.transform.position = Vector3.MoveTowards(bulletObject.transform.position, bulletPocketTarget.position, step);

            if (Vector3.Distance(bulletObject.transform.position, bulletPocketTarget.position) < 0.0001f)//< 0.001f)
            {
               Debug.Log("POCKET");
               
               bulletIn = true;
               bulletOut = false;
            }

            
        }
        if (bulletIn)
        {
            bulletObject.transform.position = Vector3.MoveTowards(bulletPocketTarget.transform.position, bulletSpawnPoint.position, 100);
            if (Vector3.Distance(bulletObject.transform.position, bulletSpawnPoint.position) < 0.0001f)//< 0.001f)
            {
                Debug.Log("SPAWN");
                bulletOut = true;
                bulletIn = false;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if(other.gameObject.tag == "Zombie") //on the object you want to pick up set the tag to be anything, in this case "object"
        {
           Debug.Log("HIT");
            
        }
    }
}
