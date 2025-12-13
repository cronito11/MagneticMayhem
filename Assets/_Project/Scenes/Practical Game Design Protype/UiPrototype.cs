using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MagneticMayhem
{
    public class UiPrototype : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
        }

        public void OnClick()
        {
           SceneManager.LoadScene("Level Prototype 1"); 
        }
    }
}
