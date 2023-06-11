using UnityEngine;
using Unity.Collections;

[CreateAssetMenu(fileName = "New Sound List", menuName = "Scriptable Objects/Sounds/Sound List")]
public class SoundList : ScriptableObject
{
    [SerializeField, ReadOnly]
    public SoundItem[] soundDetails;
}