using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoSingleton<SoundController>
{
    [SerializeField] private SoundsAsset sounds;
    [SerializeField] private Sound soundBGM;
    private AudioSource audioSource;
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        Instance.audioSource.clip = sounds[soundBGM];
        Instance.audioSource.Play();
    }
    public void Play(Sound sound)
    {
        audioSource.PlayOneShot(sounds[sound]);
    }
}