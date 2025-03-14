using Platformer397;
using System.Collections.Generic;
using UnityEngine;

namespace MagneticMayhem
{
    public class LevelConfigurationManager : Singleton<LevelConfigurationManager>
    {
        [SerializeField] private List<LevelConfiguration> levelConfigurations;

        public LevelConfiguration GetLevelConfigOf(int _level)
        {
            return levelConfigurations[_level]; 
        }
    }
}
