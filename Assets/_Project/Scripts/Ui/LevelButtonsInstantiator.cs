using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagneticMayhem
{
    public class LevelButtonsInstantiator : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        private void Start ()
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 2; i++)
            {
                Instantiate(prefab, transform);
            }
        }
    }
}
