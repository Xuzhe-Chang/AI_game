using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehaviour : StateMachineBehaviour
{

    float lastPositionY, fallDistance;
    CharacterAudio audio;
    CharacterEffect effecter;
    PlayerController character;

    private void Awake()
    {
        audio = FindObjectOfType<CharacterAudio>();
        effecter = FindObjectOfType<CharacterEffect>();
        character = FindObjectOfType<PlayerController>();
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fallDistance = 0;
        animator.SetFloat("FallDistance", fallDistance);

        audio.Play(CharacterAudio.AudioType.Falling, true);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (lastPositionY > character.transform.position.y)
        {
            fallDistance += lastPositionY - character.transform.position.y;
        }
        lastPositionY = character.transform.position.y;
        animator.SetFloat("FallDistance", fallDistance);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.Play(CharacterAudio.AudioType.Falling, false);
    }

    public void ResetAllParams()
    {
        lastPositionY = character.transform.position.y;
        fallDistance = 0;
    }
}
