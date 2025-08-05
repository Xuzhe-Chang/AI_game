using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLandBehaviour : StateMachineBehaviour
{
    CharacterAudio audio;
    CinemaShaking shake;

    private void Awake()
    {
        audio = FindObjectOfType<CharacterAudio>();
        shake = FindObjectOfType<CinemaShaking>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Play(CharacterAudio.AudioType.Landing, true);
        shake.CinemaShake();
        FindObjectOfType<SoulOrb>().DelayShowOrb(2);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("FirstLanding", true);
        FindObjectOfType<GameManager>().SetEnableInput(true);

    }
}
