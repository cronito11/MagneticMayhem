using DG.Tweening;
using Platformer397;
using System;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.HDROutputUtils;

namespace MagneticMayhem
{
    public enum SceneType
    {
        MainMenu,
        Game,
        FinalMenu
    }
    public class SceneManagerController : Singleton<SceneManagerController>
    {
        private const float FADE_TIME = 1f;
        private const float MIN_WAIT_TIME = 2f;

        public event Action<SceneType> OnSceneLoad;

        [SerializeField] private int LEVLES_AMOUNT = 3; //TODO:
        [SerializeField] private CanvasGroup loadingScreen;

        private float currentTime;
        protected override void Awake ()
        {
            base.Awake();
            HideLoadingScreen();
        }
        public void ChangeScene (string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private void ActivateLoadingScreen ()
        {
            loadingScreen.blocksRaycasts = true;
            loadingScreen.interactable = false;

            loadingScreen.DOFade(1, FADE_TIME);
        }

        private void HideLoadingScreen ()
        {
            loadingScreen.DOFade(0, FADE_TIME).OnComplete(() => {
                loadingScreen.blocksRaycasts = false;
                loadingScreen.interactable = false;
            });
        }

        IEnumerator LoadSceneAsync (string sceneName)
        {
            yield return null;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            StartCoroutine(LoadingProcess(operation, SceneManager.GetSceneByName(sceneName).buildIndex));
        }

        IEnumerator LoadingProcess (AsyncOperation operation, int sceneId)
        {
            ActivateLoadingScreen();

            currentTime = 0;
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress
                currentTime+= Time.deltaTime;
                //upload your progress bar here
                if (operation.progress >= 0.9f && currentTime >= MIN_WAIT_TIME)
                {
                    //   if (Input.anyKeyDown)
                    //   {
                    operation.allowSceneActivation = true; // Activate new scene entrance
                    HideLoadingScreen();
                    //   }
                }

                yield return null;
            }

            switch (sceneId)
            {
                case 0:
                OnSceneLoad?.Invoke(SceneType.MainMenu);
                break;
                default:
                OnSceneLoad?.Invoke(SceneType.Game);
                break;
            }

        }

        public void ChangeScene (int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        IEnumerator LoadSceneAsync (int sceneId)
        {
            yield return null;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
            StartCoroutine(LoadingProcess(operation, sceneId));
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
