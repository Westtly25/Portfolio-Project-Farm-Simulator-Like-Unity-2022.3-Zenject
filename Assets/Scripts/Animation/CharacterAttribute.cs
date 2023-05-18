[System.Serializable]
public class CharacterAttribute
{
    public CharacterPartAnimator CharacterPart { get; set; }
    public PartVariantColour PartVariantColour { get; set; }
    public PartVariantType PartVariantType { get; set; }

    public CharacterAttribute(CharacterPartAnimator characterPart, PartVariantColour partVariantColour, PartVariantType partVariantType)
    {
        CharacterPart = characterPart;
        PartVariantColour = partVariantColour;
        PartVariantType = partVariantType;
    }
}
