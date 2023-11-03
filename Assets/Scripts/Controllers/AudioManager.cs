using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource powerUpAudio;
    [SerializeField]
    private AudioSource damageAudio;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void PlayDamageSound()
    {
        damageAudio.Play();
    }

    public void PlayPowerUpSound()
    {
        powerUpAudio.Play();
    }
}
