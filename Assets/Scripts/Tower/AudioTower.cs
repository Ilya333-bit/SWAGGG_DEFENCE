using UnityEngine;

public class AudioTower : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip shootSound;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            shootSound = audioSource.clip;
        }
    }

    public void Audio()
    {
        audioSource.PlayOneShot(shootSound);
    }
}