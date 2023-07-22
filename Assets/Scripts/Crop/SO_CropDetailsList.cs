using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CropDetailsList", menuName = "Scriptable Objects/Crop/Crop Details List")]
public class SO_CropDetailsList : ScriptableObject
{
    [SerializeField]
    public List<CropDetails> cropDetails;

    public CropDetails GetCropDetails(int seedItemCode) =>
        cropDetails.Find(x => x.seedItemCode == seedItemCode);
}
