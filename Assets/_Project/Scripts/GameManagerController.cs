using Platformer397;
using System;
using System.Collections.Generic;

namespace MagneticMayhem
{
    [Serializable]
    public struct PlayerData
    {
        public AudioSettings audioConfiguration;
        public List<LevelData> levelsData;

        public PlayerData (AudioSettings config)
        {
            audioConfiguration = config;
            levelsData = new List<LevelData>();
        }
    }

    [Serializable]
    public struct LevelData
    {
        public bool pass;
        public int levelNum;
        public int stars;
    }

    public class GameManagerController : Singleton<GameManagerController>
    {
        private const int MAX_STARS = 3;
        private PlayerData _playerData;

        public PlayerData playerData => _playerData;

        protected override void Awake ()
        {
            base.Awake();
            LoadData();
        }

        private void LoadData ()
        {
            _playerData = new PlayerData (new AudioSettings {
                isMuted = false,
                isMusicMute = false,
                isSFXMute = false,
                generalVolume = 1,
                sfxVolume = 1,

            });
        }

        public void CompleteLevel (int level, int stars)
        {
            if (stars <= 0 || stars > MAX_STARS)
            {
                throw new ArgumentException("The number of stars summited is invalid");
            }
            int index = _playerData.levelsData.FindIndex(x => x.levelNum == level);

            if (index == -1)
            {
                _playerData.levelsData.Add(new LevelData {
                    stars = stars,
                    pass = true,
                    levelNum = level
                });
            }
            else 
            {
                if (_playerData.levelsData [index].stars < stars)
                {
                    LevelData updatedLevel = _playerData.levelsData[index];
                    updatedLevel.stars = stars;
                    _playerData.levelsData [index] = updatedLevel;
                }
            }
        }
    }
}
