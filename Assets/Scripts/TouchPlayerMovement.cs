using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniversalMobileController;

public class TouchPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity;
    float gravity = -9.81f;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    float jumpHeight = 2f;

   
    private int fingerID;
    private Vector2 moveInput;
    private float halfScreenWidth;

    private Vector2 lookInput;
    public float cameraSensivity;
    private float cameraPitch;
    public Transform characterCamera;
    public FloatingJoyStick joystick;
    public Animator playerAnimator;

    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;

    void Start()
    {
        fingerID = -1;
        halfScreenWidth = Screen.width / 2;
       

    }

     
    void Update()
    {
        if (fingerID != -1)
        {
            LookAround();
        }

        Move();
        getLookInput();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void getLookInput()
    {
        

        for (int i=0;i<Input.touchCount;i++)
        {
            Touch t = Input.GetTouch(i);

            if(t.phase==TouchPhase.Began )
            {
                if (t.position.x > halfScreenWidth && fingerID == -1)
                {
                    fingerID = t.fingerId;
                }
            }

            if (t.phase == TouchPhase.Moved)
            {
                if(fingerID==t.fingerId)
                {
                    lookInput = t.deltaPosition * cameraSensivity * Time.deltaTime;
                }
            }

            if (t.phase == TouchPhase.Stationary)
            {
                if(t.fingerId==fingerID)
                {
                    lookInput = Vector2.zero;
                }
            }

            if (t.phase == TouchPhase.Ended)
            {
                if(fingerID==t.fingerId)
                {
                    fingerID = -1;
                }
            }
        }
    }

    void Move()
    {
        
        moveInput = joystick.GetHorizontalAndVerticalValue();
        Vector3 move = transform.right *moveInput.normalized.x + transform.forward * moveInput.normalized.y;
        controller.Move(move * speed * Time.deltaTime);
        
        playerAnimator.SetFloat("Speed", move.magnitude);
        transform.Rotate(transform.up, moveInput.normalized.x*2);
    }

    void LookAround()
    {
        cameraPitch = Mathf.Clamp(cameraPitch-lookInput.y, -30, 33);

        characterCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        transform.Rotate(transform.up, lookInput.x);




    }

    public void Jump()
    {
        playerAnimator.SetTrigger("Jump");
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    public void Sprint()
    {
        if (speed == walkSpeed)

            speed = sprintSpeed;

        else
            speed = walkSpeed;

    }
}
