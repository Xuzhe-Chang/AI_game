using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBehaviour : StateMachineBehaviour
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
        audio.Play(CharacterAudio.AudioType.Slide, true);
        effecter.DoEffect(CharacterEffect.EffectType.Slide, true);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Play(CharacterAudio.AudioType.Slide, false);
        effecter.DoEffect(CharacterEffect.EffectType.Slide, false);
    }

}
