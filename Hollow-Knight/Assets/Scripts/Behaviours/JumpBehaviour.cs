using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : StateMachineBehaviour
{
    CharacterEffect effecter;

    private void Awake()
    {
        effecter = FindObjectOfType<CharacterEffect>();
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        effecter.DoEffect(CharacterEffect.EffectType.DustJump, true);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        effecter.DoEffect(CharacterEffect.EffectType.DustJump, false);
    }

}
