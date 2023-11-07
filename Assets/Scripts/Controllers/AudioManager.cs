using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Attributes
    public static AudioManager instance;

    [SerializeField]
    private AudioSource powerUpAudio;
    [SerializeField]
    private AudioSource damageAudio;
    #endregion

    #region Unity Functions
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
    #endregion

    #region Sound Functions
    public void PlayDamageSound()
    {
        damageAudio.Play();
    }

    public void PlayPowerUpSound()
    {
        powerUpAudio.Play();
    }
    #endregion
}
