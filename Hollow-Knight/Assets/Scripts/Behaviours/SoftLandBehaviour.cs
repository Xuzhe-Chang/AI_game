using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftLandBehaviour : StateMachineBehaviour
{
    CharacterAudio audio;

    private void Awake()
    {
        audio = FindObjectOfType<CharacterAudio>();

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Play(CharacterAudio.AudioType.Landing, true);
    }
}
