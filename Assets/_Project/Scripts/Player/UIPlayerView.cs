using TMPro;
using UnityEngine;

namespace MagneticMayhem
{
    public class UIPlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;

        private Canvas canvas;
        private PlayerController playerController;


        private void Awake ()
        {
            canvas = GetComponentInParent<Canvas>();
            playerController = GetComponentInParent<PlayerController> ();
        }

        void Start()
        {
            canvas.worldCamera = Camera.main;
            playerName.SetText($"P{((int)playerController.playerIdentifier)+1}");
            playerName.color = playerController.playerIdentifier == Player.playerOne ? Color.blue : Color.red;
        }
    }
}
