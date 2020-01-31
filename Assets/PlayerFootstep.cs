using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{

    protected int m_CurrentFootstepSoundIndex = 0;
    [SerializeField]
    protected AudioSource m_FootstepAudioSource;

    void Start()
    {

    }


    public virtual void PlayFootstepSound()
    {
        int isGrounded = PlayerPrefs.GetInt("IsGrounded");
        if (isGrounded == 1)
        {
            AudioManager.Singleton.PlayFootstepSound(m_FootstepAudioSource, ref m_CurrentFootstepSoundIndex);
        }
    }
}
