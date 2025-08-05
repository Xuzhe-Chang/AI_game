using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviour : StateMachineBehaviour
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
        audio.Play(CharacterAudio.AudioType.Walk, true);
        effecter.DoEffect(CharacterEffect.EffectType.DustWalk, true);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Play(CharacterAudio.AudioType.Walk, false);
        effecter.DoEffect(CharacterEffect.EffectType.DustWalk, false);
    }


}
