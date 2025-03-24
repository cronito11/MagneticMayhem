using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MagneticMayhem
{
    public class BtnNextLevel : MonoBehaviour
    {
        private Button button;

        private void Awake ()
        {
            button = GetComponent<Button>();
        }

        private void Start ()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDestroy ()
        {
            button.onClick.RemoveListener(OnClick);
        }

        public void OnClick ()
        {
            SceneManagerController.Instance.NextLevel();
        }
    }
}
