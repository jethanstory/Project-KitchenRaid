using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabKnifeScr : MonoBehaviour
{
    public GameObject knifeHand;

    public float speed = 2.0f;

    // The target (cylinder) position.
    public Transform knifeTarget;
    public Transform pocketTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //StabKnife();
        // if (Input.GetMouseButtonDown(0))
        if (Input.GetKeyDown(KeyCode.V))
        {
            StabKnife();
        }
    }

    public void StabKnife()
    {
        {
            var step =  speed * Time.deltaTime; // calculate distance to move
            knifeHand.transform.position = Vector3.MoveTowards(knifeHand.transform.position, knifeTarget.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(knifeHand.transform.position, knifeTarget.position) < 0.0001f)//< 0.001f)
            {
               Debug.Log("HIT");
               knifeHand.transform.position = Vector3.MoveTowards(knifeHand.transform.position, pocketTarget.position, step);
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
