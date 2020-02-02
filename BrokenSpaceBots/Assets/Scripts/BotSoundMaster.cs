using UnityEngine;

public class BotSoundMaster : MonoBehaviour
{
    public BotSoundReference soundRef;
    public AudioSource voiceAudioSource;
    public AudioSource movementAudioSource;

    enum SoundLoopMode
    {
        Nothing,
        Fixing,
        Moving
    };

    SoundLoopMode soundLoopMode = SoundLoopMode.Nothing;

    public void PlayHappySound()
    {
        AudioClip fixSound = soundRef.botFixedSounds[Random.Range(0, soundRef.botFixedSounds.Length)];

        voiceAudioSource.Stop();
        voiceAudioSource.clip = fixSound;
        voiceAudioSource.Play();
    }

    public void PlayComplainSound()
    {
        AudioClip sound = soundRef.botComplainSounds[Random.Range(0, soundRef.botComplainSounds.Length)];

        voiceAudioSource.Stop();
        voiceAudioSource.clip = sound;
        voiceAudioSource.Play();
    }

    public void PlayPanelSound()
    {
        AudioClip sound = soundRef.botPanelSounds[Random.Range(0, soundRef.botPanelSounds.Length)];

        voiceAudioSource.Stop();
        voiceAudioSource.clip = sound;
        voiceAudioSource.Play();
    }

    public void StartMoving()
    {
        if (soundLoopMode != SoundLoopMode.Moving)
        {
            AudioClip sound = soundRef.botMovementSounds[Random.Range(0, soundRef.botMovementSounds.Length)];

            movementAudioSource.Stop();
            movementAudioSource.clip = sound;
            movementAudioSource.Play();

            soundLoopMode = SoundLoopMode.Moving;
        }
    }

    public void StartFixing()
    {
        if (soundLoopMode != SoundLoopMode.Fixing)
        {
            AudioClip sound = soundRef.botWeldingSounds[Random.Range(0, soundRef.botWeldingSounds.Length)];

            movementAudioSource.Stop();
            movementAudioSource.clip = sound;
            movementAudioSource.Play();

            soundLoopMode = SoundLoopMode.Fixing;
        }
    }

    public void StopLoop()
    {
        movementAudioSource.Stop();
        soundLoopMode = SoundLoopMode.Nothing;
    }
}
