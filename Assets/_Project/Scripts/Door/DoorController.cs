using UnityEngine;
using UnityEngine.SceneManagement;

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
                    playerExit--;
                    if (playerExit == 0)
                    {
                        GameManagerController.Instance.CompleteLevel(SceneManager.GetActiveScene().buildIndex, 3);
                        SceneManagerController.Instance.NextLevel();
                    }
                }
            }
        }

        private void OnTriggerExit2D (Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player.playerIdentifier == playerIdentifier)
                {
                    playerExit++;
                }
            }
        }
    }
}
