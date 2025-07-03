using UnityEngine;

public class SoundEffect : NewMonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audioSource.Play();

    }
}
