using System.Collections.Generic;
using UnityEngine;

namespace MagneticMayhem
{
    [CreateAssetMenu(fileName = "LevelConfigurableSO", menuName = "Scriptable Objects/LevelConfigurableSO")]
    public class LevelConfigurableSO : ScriptableObject
    {
        public List<ConfigurableSO> configurableSOs = new List<ConfigurableSO>(); 
    }
}
