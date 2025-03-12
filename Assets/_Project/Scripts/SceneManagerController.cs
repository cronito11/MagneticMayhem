using Platformer397;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace MagneticMayhem
{
    public class SceneManagerController : Singleton<SceneManagerController>
    {
        [SerializeField] private int LEVLES_AMOUNT = 3; //TODO:

        public void ChangeScene (string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        IEnumerator LoadSceneAsync (string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress
                //upload your progress bar here
                if (operation.progress >= 0.9f)
                {
                 //   if (Input.anyKeyDown)
                 //   {
                        operation.allowSceneActivation = true; // Activate new scene entrance
                 //   }
                }

                yield return null;
            }
        }

        public void ChangeScene (int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        IEnumerator LoadSceneAsync (int sceneId)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress
                //upload your progress bar here
                if (operation.progress >= 0.9f)
                {
                    //   if (Input.anyKeyDown)
                    //   {
                    operation.allowSceneActivation = true; // Activate new scene entrance
                                                           //   }
                }

                yield return null;
            }
        }

        public void NextLevel ()
        {
            if (LEVLES_AMOUNT == GameManagerController.Instance.playerData.levelsData.Count)
                SceneManagerController.Instance.ChangeScene(0);
            else
                SceneManagerController.Instance.ChangeScene(GameManagerController.Instance.playerData.levelsData.Count +1);
        }
    }
}
