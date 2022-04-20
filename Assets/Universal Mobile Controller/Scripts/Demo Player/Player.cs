using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMobileController
{
    public class Player : MonoBehaviour
    {
        private float horizontalInput = 0f;
        private float verticalInput = 0f;
        private float movementSpeed = 2;
        private Rigidbody rb;
        [SerializeField] private FloatingJoyStick joystick;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            horizontalInput = joystick.GetHorizontalValue();
            verticalInput = joystick.GetVerticalValue();
            
            transform.Translate(Vector3.forward.normalized * horizontalInput * movementSpeed  * Time.deltaTime);
            transform.Translate(Vector3.right.normalized * - verticalInput * movementSpeed  * Time.deltaTime);
        }
        public void Jump()
        {
            rb.AddForce(Vector3.up * 150 * Time.deltaTime, ForceMode.Impulse);    
        }
    }
}
