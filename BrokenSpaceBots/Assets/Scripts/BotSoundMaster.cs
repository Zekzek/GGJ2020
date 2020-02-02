using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSoundMaster : MonoBehaviour
{
    public BotSoundReference soundRef;
    public AudioSource voiceAudioSource;
    public AudioSource movementAudioSource;

    public void PlayFixSound()
    {
        AudioClip fixSound = soundRef.botFixedSounds[Random.Range(0, soundRef.botFixedSounds.Length)];

        voiceAudioSource.Stop();
        voiceAudioSource.loop = false;
        voiceAudioSource.clip = fixSound;
        voiceAudioSource.Play();
    }

    public void PlayBrokenSound()
    {
        AudioClip brokenSound = soundRef.botBrokenSounds[Random.Range(0, soundRef.botBrokenSounds.Length)];

        voiceAudioSource.Stop();
        voiceAudioSource.loop = false;
        voiceAudioSource.clip = brokenSound;
        voiceAudioSource.Play();
    }

    public void StartMoving()
    {
        AudioClip brokenSound = soundRef.botBrokenSounds[Random.Range(0, soundRef.botBrokenSounds.Length)];
    }
}
