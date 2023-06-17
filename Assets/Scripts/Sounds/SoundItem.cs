using UnityEngine;

[System.Serializable]
public class SoundItem
{
    [SerializeField]
    private SoundName soundName;

    [SerializeField]
    private AudioClip soundClip;

    [SerializeField, Range(0.1f, 1.5f)]
    private float soundPitchRandomVariationMin = 0.8f;

    [SerializeField, Range(0.1f, 1.5f)]
    private float soundPitchRandomVariationMax = 1.2f;

    [SerializeField, Range(0f, 1f)]
    private float soundVolume = 1f;

    public SoundName SoundName
    {
        get => soundName;
        set => soundName = value;
    }
    public AudioClip SoundClip
    {
        get => soundClip;
        set => soundClip = value;
    }
    public string SoundDescription { get; set; }
    public float SoundPitchRandomVariationMin
    {
        get => soundPitchRandomVariationMin;
        set => soundPitchRandomVariationMin = value;
    }
    public float SoundPitchRandomVariationMax
    {
        get => soundPitchRandomVariationMax;
        set => soundPitchRandomVariationMax = value;
    }
    public float SoundVolume
    {
        get => soundVolume;
        set => soundVolume = value;
    }
}
