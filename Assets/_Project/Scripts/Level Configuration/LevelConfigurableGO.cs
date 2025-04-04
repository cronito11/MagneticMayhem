using System.Collections.Generic;
using UnityEngine;

namespace MagneticMayhem
{
    [CreateAssetMenu(fileName = "LevelConfigurableGO", menuName = "Scriptable Objects/LevelConfigurableGO")]
    public class LevelConfigurableGO : ScriptableObject
    {
        public List<ConfigurableGO> configurableGOs = new List<ConfigurableGO>(); 
    }
}
