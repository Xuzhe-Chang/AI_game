using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{

    [SerializeField] AudioSource mainAudio;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource landingAudio;
    [SerializeField] AudioSource hardLandingAudio;
    [SerializeField] AudioSource fallingAudio;
    [SerializeField] AudioSource takeDamageAudio;
    [SerializeField] AudioSource rejectSwordHit;
    [SerializeField] AudioSource walkAudio;
    [SerializeField] AudioSource runAudio;
    [SerializeField] AudioSource slideAudio;
    [SerializeField] AudioSource slideJumpAudio;


    public enum AudioType
    {
        Jump,Landing,Falling,TakeDamage,RejectHit,Walk,Run,Slide,SlideJump,HardLand
    }

    public void Play(AudioType audioType, bool playState)
    {
        AudioSource audioSource = null;
        switch (audioType)
        {
            case AudioType.Jump:
                audioSource = jumpAudio;
                break;

            case AudioType.Landing:
                audioSource = landingAudio;
                break;

            case AudioType.Falling:
                audioSource = fallingAudio;
                break;

            case AudioType.TakeDamage:
                audioSource = takeDamageAudio;
                break;

            case AudioType.RejectHit:
                audioSource = rejectSwordHit;
                break;

            case AudioType.Walk:
                audioSource = walkAudio;
                break;

            case AudioType.Run:
                audioSource = runAudio;
                break;

            case AudioType.Slide:
                audioSource = slideAudio;
                break;

            case AudioType.SlideJump:
                audioSource = slideJumpAudio;
                break;

            case AudioType.HardLand:
                audioSource = hardLandingAudio;
                break;


        }





        if (audioSource != null)
        {
            if (playState)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
}
