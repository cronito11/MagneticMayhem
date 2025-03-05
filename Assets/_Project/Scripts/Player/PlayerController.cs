using System;
using UnityEngine;

public enum Player { playerOne = 0, playerTwo = 1}; //This is a enum that is used to identify the player

namespace MagneticMayhem
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private bool groundCheck;
        
        [SerializeField] private InputReader input;

        [field: SerializeField] public Player playerIdentifier { get; private set; }

        private Vector3 movement;
        private IMageneticPoleChangeable switchPolarity;

        void Awake()
        {
            switchPolarity = GetComponent<IMageneticPoleChangeable>();
        }

        private void Start()
        {
            input.EnablePlayerActions();    
        }

        private void OnEnable()
        {
            input.Move += GetMovement;
            input.Interact += GetInteraction;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //take input from player
            if(groundCheck)
                UpdateMovement();
        }

        private void OnDisable()
        {
            input.Move -= GetMovement;
            input.Interact -= GetInteraction;
        }

        private void GetInteraction ()
        {
            switchPolarity.Switch();
        }

        private void GetMovement(Vector2 move)
        {
            movement.x = move.x;
            movement.y = move.y;    
            //move player
            Debug.Log(move, gameObject);  
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
