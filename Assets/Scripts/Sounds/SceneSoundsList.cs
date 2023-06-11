using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Scene Sounds List", menuName = "Scriptable Objects/Sounds/Scene Sounds List")]
public class SceneSoundsList : ScriptableObject
{
    [SerializeField, ReadOnly]
    public List<SceneSoundsItem> sceneSoundsDetails;
}
