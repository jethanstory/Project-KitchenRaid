using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScr : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 3f; //12 //3 Remain //3.5?
    public float gravity = -9.81f;
    public float jumpHeight = 1f; //3

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float sprintTime = 7000; //1000 //300

    bool canSprint;


    Vector3 velocity;
    public bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKey("left shift") && canSprint == true)//sprintTime >= 2) //== 3000) //>= 2) //&& isGrounded)
        {
            speed = 7f; //20
            sprintTime += -20;
            if (sprintTime < 0)
                canSprint = false;

        }
        
    
        else
        {
            speed = 3f; //12
            
            if (sprintTime < 7000)
                sprintTime += 20; //1
           else if (sprintTime == 7000)
                canSprint = true;
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
