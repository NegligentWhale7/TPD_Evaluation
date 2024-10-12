using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource footSource;
    private AudioSource audioSource;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundFX(SoundType soundType, float volume = 1)
    {
        audioSource.PlayOneShot(audioClips[(int)soundType], volume);
    }

    public void PlayFootSteps()
    {
        footSource.Play();
    }
}

public enum SoundType
{
    Footstep,
    ShootingBullet,
    ShootingRocket,
    Explosion,
    ShieldDamaged,
    Hurt,
    Coin,
    Score,
    GameOver,
    Victory
}
