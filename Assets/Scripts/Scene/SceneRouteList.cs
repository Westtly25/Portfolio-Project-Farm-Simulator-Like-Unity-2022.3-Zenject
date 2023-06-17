using UnityEngine;
using Unity.Collections;

[CreateAssetMenu(fileName = "New Scene Route List", menuName = "Scriptable Objects/Scene/Scene Route List")]
public class SceneRouteList : ScriptableObject
{
    [SerializeField, ReadOnly]
    public SceneRoute[] sceneRouteList;
}