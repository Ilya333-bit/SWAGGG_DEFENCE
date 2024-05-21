using UnityEngine;

public class AudioEnemy: MonoBehaviour
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
