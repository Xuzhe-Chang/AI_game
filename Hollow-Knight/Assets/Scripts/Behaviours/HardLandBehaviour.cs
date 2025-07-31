using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardLandBehaviour : StateMachineBehaviour
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
    }
}
