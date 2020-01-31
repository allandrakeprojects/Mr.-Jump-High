using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Singleton;

    public static AudioManager Singleton
    {
        get
        {
            return m_Singleton;
        }
    }

    [Header("Sound Clips")]
    [Space]
    [SerializeField]
    protected AudioClip m_CoinSound;
    [SerializeField]
    protected AudioClip m_ChestSound;
    [SerializeField]
    protected AudioClip m_WaterSplashSound;
    [SerializeField]
    protected AudioClip m_SpikeSound;
    [SerializeField]
    protected AudioClip[] m_GroundedSounds;
    [SerializeField]
    protected AudioClip m_JumpSound;
    [SerializeField]
    protected AudioClip[] m_FootstepSounds;
    void Awake()
    {
        m_Singleton = this;
        //PlayMusic();
    }

    public void PlaySoundOn(AudioSource audio, AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }

    public void PlayFootstepSound(AudioSource audio, ref int index)
    {
        if (m_FootstepSounds.Length > 0)
        {
            PlaySoundOn(audio, m_FootstepSounds[index]);
            if (index < m_FootstepSounds.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
    }
}
