using UnityEngine;

namespace MagneticMayhem
{
    public class FinalSceneNextLevel : MonoBehaviour
    {
        public void NextLevel ()
        {
            GameManagerController.Instance.CompleteGame();
            SceneManagerController.Instance.ChangeScene(0);
        }
    }
}
