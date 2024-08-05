using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public List<AudioClipData> clipList;
    public AudioSource sfxSource;
    public AudioSource bgSource;

    private Dictionary<AudioType, List<AudioClip>> audioClipsDict = new Dictionary<AudioType, List<AudioClip>>();

    private void Start()
    {
        foreach (AudioClipData data in clipList)
        {
            audioClipsDict.Add(data.audioType, data.audioClips);
        }
        bgSource.loop = true;
    }

    public void Play(AudioType audioType, AudioSource audioSource = null)
    {
        if (audioSource == null)
        {
            audioSource = sfxSource;
        }
        if (audioClipsDict.ContainsKey(audioType))
        {
            audioSource.PlayOneShot(GetAudioForType(audioType));
        }
    }
    public void PlayBG(AudioType audioType)
    {
        if (audioClipsDict.ContainsKey(audioType))
        {
            StartCoroutine(FadeOutAndIn(bgSource, GetAudioForType(audioType)));
        }
    }
    public void StopBG()
    {
        if (bgSource.isPlaying)
        {
            bgSource.Stop();
        }
    }
    public void SetMusicMute(bool isMute)
    {
        bgSource.mute = isMute;
        SavedDataHandler.instance._saveData.isMusicMute = isMute;
    }
    public void SetSFXMute(bool isMute)
    {
        sfxSource.mute = isMute;
        SavedDataHandler.instance._saveData.isSFXMute = isMute;
    }
    public AudioClip GetAudioForType(AudioType audioType)
    {
        if (audioClipsDict.ContainsKey(audioType))
        {
            return audioClipsDict[audioType][UnityEngine.Random.Range(0, audioClipsDict[audioType].Count)];
        }
        return null;
    }

    private IEnumerator FadeOutAndIn(AudioSource source, AudioClip newClip, float transitionDuration = 1f)
    {
        // Fade out the current clip
        float startVolume = source.volume;
        if (source.isPlaying)
        {
            for (float t = 0; t < transitionDuration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(startVolume, 0, t / transitionDuration);
                yield return null;
            }
        }

        // Switch to the new clip
        source.Stop();
        source.clip = newClip;
        source.Play();

        // Fade in the new clip
        for (float t = 0; t < transitionDuration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(0, startVolume, t / transitionDuration);
            yield return null;
        }

        source.volume = startVolume;
    }
}

public enum AudioType
{
    None,
    UIButton,
    LevelComplete,
    TapOnPiece,
    TapRestrict,
    BulbOn,
    Background
}
[System.Serializable]
public class AudioClipData
{
    public AudioType audioType;
    public List<AudioClip> audioClips;
}