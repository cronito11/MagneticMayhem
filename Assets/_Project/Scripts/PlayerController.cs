using System;
using UnityEngine;

namespace MagneticMayhem
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private bool groundCheck;
        
        [SerializeField] private InputReader input;

        private float horizontal;
        private Vector3 movement;

        void Awake()
        {
            
            
        }

        private void Start()
        {
            input.EnablePlayerActions();    
        }

        private void OnEnable()
        {
            input.Move += GetMovement; 
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //take input from player   
            UpdateMovement();
        }

        private void OnDisable()
        {
            input.Move -= GetMovement;
        }

        private void TakeInput()
        {
            //take input from player
            horizontal = Input.GetAxis("Horizontal");
        }

        private void GetMovement(Vector2 move)
        {
            movement.x = move.x;
            movement.y = move.y;    
            //move player
            Debug.Log(move);  
        }

        private void UpdateMovement()
        {
            //move player
            transform.Translate(Vector2.right * movement.x * moveSpeed * Time.deltaTime);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            //check if player is grounded
            if(other.gameObject.CompareTag("Ground"))
            {
                groundCheck = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            //check if player is grounded and esitting from the ground collider 
            if (other.gameObject.CompareTag("Ground"))
            {
                groundCheck = false;
            }
        }

    }
}
