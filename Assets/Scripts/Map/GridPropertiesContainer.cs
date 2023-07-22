using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Map
{
    [CreateAssetMenu(fileName = "New Grid Properties Container", menuName = "Scriptable Object / Grid Properties Container")]
    public class GridPropertiesContainer : ScriptableObject
    {
        [SerializeField]
        public SceneName SceneName;
        [SerializeField]
        public int gridWidth;
        [SerializeField]
        public int gridHeight;
        [SerializeField]
        public int originX;
        [SerializeField]
        public int originY;

        [SerializeField, ReadOnly]
        public List<GridProperty> Properties;
    }
}
