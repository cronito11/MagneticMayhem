using UnityEngine;

namespace MagneticMayhem
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private static int playerExit = 2; 
        [SerializeField] private Player playerIdentifier;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player.playerIdentifier == playerIdentifier)
                {
                    Debug.Log("Player " + playerIdentifier + " entered the door");
                    collision.gameObject.SetActive(false);
                    playerExit--;
                    if (playerExit == 0)
                    {
                        Debug.Log("level Complete.");
                        //trigger event from game manager
                    }
                }
            }
        }
    }
}
