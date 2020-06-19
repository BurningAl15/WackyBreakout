using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Gets whether or not the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.HitSound, 
            Resources.Load<AudioClip>("HitSound"));
        audioClips.Add(AudioClipName.PaddleHit,
            Resources.Load<AudioClip>("PaddleHit"));
        audioClips.Add(AudioClipName.PickupFreeze,
            Resources.Load<AudioClip>("PickupFreeze"));
        audioClips.Add(AudioClipName.PickupSpeedUp,
             Resources.Load<AudioClip>("PickupSpeedUp"));
        audioClips.Add(AudioClipName.Spawn,
             Resources.Load<AudioClip>("Spawn"));
        audioClips.Add(AudioClipName.BallHitBall,
              Resources.Load<AudioClip>("BallHitBall"));
        audioClips.Add(AudioClipName.DestroyObj,
            Resources.Load<AudioClip>("DestroyObj"));
        audioClips.Add(AudioClipName.Click,
            Resources.Load<AudioClip>("Click"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
