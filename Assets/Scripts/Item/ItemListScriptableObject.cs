using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName= "ItemListScriptableObject", menuName="Scriptable Objects/Item/Item List")]
public class ItemListScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<ItemDetails> itemDetails;
}