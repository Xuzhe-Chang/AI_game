using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBehaviour : StateMachineBehaviour
{
    CharacterAudio audio;
    CharacterEffect effecter;

    private void Awake()
    {
        audio = FindObjectOfType<CharacterAudio>();
        effecter = FindObjectOfType<CharacterEffect>();
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Play(CharacterAudio.AudioType.Run, true);
        effecter.DoEffect(CharacterEffect.EffectType.DustRun, true);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Play(CharacterAudio.AudioType.Run, false);
        effecter.DoEffect(CharacterEffect.EffectType.DustRun, false);
    }


}
