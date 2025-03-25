using System;
using UnityEngine;
using UnityEngine.UI;

namespace MagneticMayhem
{
    public class LevelSelectionController : MonoBehaviour
    {
        private Button button;

        private void Awake ()
        {
            button = GetComponent<Button>();
        }

        private void Start ()
        {
            button.onClick.AddListener(OnClick);
            button.interactable = GameManagerController.Instance.IsLevelUnlocked(transform.GetSiblingIndex());
        }

        private void OnDestroy ()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick ()
        {
            SceneManagerController.Instance.ChangeScene(transform.GetSiblingIndex()+1);
        }
    }
}
