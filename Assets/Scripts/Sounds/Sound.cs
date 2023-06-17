using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetSound(SoundItem soundItem)
    {
        audioSource.pitch = Random.Range(soundItem.SoundPitchRandomVariationMin, soundItem.SoundPitchRandomVariationMax);
        audioSource.volume = soundItem.SoundVolume;
        audioSource.clip = soundItem.SoundClip;
    }

    private void OnEnable()
    {
        if (audioSource.clip != null)
            audioSource.Play();
    }

    private void OnDisable()
    {
        audioSource.Stop();
    }
}