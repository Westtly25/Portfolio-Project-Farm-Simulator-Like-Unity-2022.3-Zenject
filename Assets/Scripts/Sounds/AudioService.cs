using System;
using Zenject;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UniRx;

public class AudioService : IInitializable, IDisposable
{
    [SerializeField] private GameObject soundPrefab = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambientSoundAudioSource = null;

    [SerializeField] private AudioSource gameMusicAudioSource = null;

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer gameAudioMixer = null;

    [Header("Audio Snapshots")]
    [SerializeField] private AudioMixerSnapshot gameMusicSnapshot = null;

    [SerializeField] private AudioMixerSnapshot gameAmbientSnapshot = null;

    [Header("Other")]
    [SerializeField] private SoundList so_soundList = null;

    [SerializeField] private SceneSoundsList so_sceneSoundsList = null;
    [SerializeField] private float defaultSceneMusicPlayTimeSeconds = 120f;
    [SerializeField] private float sceneMusicStartMinSecs = 20f;
    [SerializeField] private float sceneMusicStartMaxSecs = 40f;
    [SerializeField] private float musicTransitionSecs = 8f;


    private Dictionary<SoundName, SoundItem> soundDictionary;
    private Dictionary<SceneName, SceneSoundsItem> sceneSoundsDictionary;

    private Coroutine playSceneSoundsCoroutine;

    public void Initialize()
    {
        soundDictionary = new Dictionary<SoundName, SoundItem>();

        foreach (SoundItem soundItem in so_soundList.soundDetails)
        {
            soundDictionary.Add(soundItem.soundName, soundItem);
        }

        sceneSoundsDictionary = new Dictionary<SceneName, SceneSoundsItem>();

        foreach (SceneSoundsItem sceneSoundsItem in so_sceneSoundsList.sceneSoundsDetails)
        {
            sceneSoundsDictionary.Add(sceneSoundsItem.sceneName, sceneSoundsItem);
        }


    }

    public void Dispose()
    {
        EventHandler.AfterSceneLoadEvent -= PlaySceneSounds;
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += PlaySceneSounds;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= PlaySceneSounds;
    }

    private void PlaySceneSounds()
    {
        SoundItem musicSoundItem = null;
        SoundItem ambientSoundItem = null;

        float musicPlayTime = defaultSceneMusicPlayTimeSeconds;

        // Try Get current scene
        if (Enum.TryParse<SceneName>(SceneManager.GetActiveScene().name, true, out SceneName currentSceneName))
        {
            // Get Music and Ambient Sounds For Scene
            if (sceneSoundsDictionary.TryGetValue(currentSceneName, out SceneSoundsItem sceneSoundsItem))
            {
                soundDictionary.TryGetValue(sceneSoundsItem.musicForScene, out musicSoundItem);
                soundDictionary.TryGetValue(sceneSoundsItem.ambientSoundForScene, out ambientSoundItem);
            }
            else
            {
                return;
            }

            //if (playSceneSoundsCoroutine != null)
            //     StopCoroutine(playSceneSoundsCoroutine);

            playSceneSoundsCoroutine = MainThreadDispatcher.StartCoroutine(PlaySceneSoundsRoutine(musicPlayTime, musicSoundItem, ambientSoundItem));
        }
    }

    private IEnumerator PlaySceneSoundsRoutine(float musicPlaySeconds, SoundItem musicSoundItem, SoundItem ambientSoundItem)
    {

        if (musicSoundItem != null && ambientSoundItem != null)
        {
            PlayAmbientSoundClip(ambientSoundItem, 0f);

            yield return new WaitForSeconds(UnityEngine.Random.Range(sceneMusicStartMinSecs, sceneMusicStartMaxSecs));

            PlayMusicSoundClip(musicSoundItem, musicTransitionSecs);

            yield return new WaitForSeconds(musicPlaySeconds);

            PlayAmbientSoundClip(ambientSoundItem, musicTransitionSecs);
        }
    }

    private void PlayMusicSoundClip(SoundItem musicSoundItem, float transitionTimeSeconds)
    {
        gameAudioMixer.SetFloat("MusicVolume", ConvertSoundVolumeDecimalFractionToDecibels(musicSoundItem.soundVolume));

        gameMusicAudioSource.clip = musicSoundItem.soundClip;
        gameMusicAudioSource.Play();

        gameMusicSnapshot.TransitionTo(transitionTimeSeconds);
    }

    private void PlayAmbientSoundClip(SoundItem ambientSoundItem, float transitionTimeSeconds)
    {
        gameAudioMixer.SetFloat("AmbientVolume", ConvertSoundVolumeDecimalFractionToDecibels(ambientSoundItem.soundVolume));

        ambientSoundAudioSource.clip = ambientSoundItem.soundClip;
        ambientSoundAudioSource.Play();

        gameAmbientSnapshot.TransitionTo(transitionTimeSeconds);
    }

    private float ConvertSoundVolumeDecimalFractionToDecibels(float volumeDecimalFraction) =>
        (volumeDecimalFraction * 100f - 80f);


    public void PlaySound(SoundName soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out SoundItem soundItem) && soundPrefab != null)
        {
            GameObject soundGameObject = PoolManager.Instance.ReuseObject(soundPrefab, Vector3.zero, Quaternion.identity);

            Sound sound = soundGameObject.GetComponent<Sound>();

            sound.SetSound(soundItem);
            soundGameObject.SetActive(true);
            MainThreadDispatcher.StartCoroutine(DisableSound(soundGameObject, soundItem.soundClip.length));
        }
    }

    private IEnumerator DisableSound(GameObject soundGameObject, float soundDuration)
    {
        yield return new WaitForSeconds(soundDuration);
        soundGameObject.SetActive(false);
    }
}