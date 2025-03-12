using UnityEngine.SceneManagement;

namespace Platformer397
{
    public class SceneController : Singleton<SceneController>
    {
        public void ChangeScene (string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
