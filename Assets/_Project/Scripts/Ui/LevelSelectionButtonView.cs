using TMPro;
using UnityEngine;

namespace MagneticMayhem
{
    public class LevelSelectionButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private GameObject lockIcon;

        private void Start ()
        {
            levelText.SetText($"Lv. {transform.GetSiblingIndex()+1}");
            lockIcon.SetActive(!GameManagerController.Instance.IsLevelUnlocked(transform.GetSiblingIndex()));
        }
    }
}
