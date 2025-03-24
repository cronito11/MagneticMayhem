using System;
using UnityEngine;

namespace MagneticMayhem
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] AudioSource mainMenuMusic;
        [SerializeField] AudioSource gameMenuMusic;
        [SerializeField] AudioSource finalMenuMusic;

        private SceneManagerController sceneManagerController;

        private void Start ()
        {
            SceneManagerController.Instance.OnSceneLoad += OnSceneLoad;
        }

        private void OnDestroy ()
        {
            SceneManagerController.Instance.OnSceneLoad -= OnSceneLoad;
        }

        private void OnSceneLoad (SceneType type)
        {
            switch (type)
            {
                case SceneType.MainMenu:
                    mainMenuMusic.Play();
                    gameMenuMusic.Stop();
                    finalMenuMusic.Stop();
                break;
                case SceneType.FinalMenu:
                    mainMenuMusic.Stop();
                    gameMenuMusic.Stop();
                    finalMenuMusic.Play();
                break;
                default:
                    mainMenuMusic.Stop();
                    gameMenuMusic.Play();
                    finalMenuMusic.Stop();
                break;
            }
        }
    }
}
