using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;

namespace Assets.Scripts.Map
{
    [CreateAssetMenu(fileName = "New Grid Properties Container", menuName = "Scriptable Object / Grid Properties Container")]
    public class GridPropertiesContainer : ScriptableObject
    {
        [SerializeField, ReadOnly]
        public List<GridProperty> Properties;
    }
}
