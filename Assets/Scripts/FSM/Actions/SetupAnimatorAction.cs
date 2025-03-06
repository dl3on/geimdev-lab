using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupAnimator")]
public class SetupAnimator : Action
{
    public RuntimeAnimatorController animatorController;
    public AudioClip stateChangeStart;
    public override void Act(StateController controller)
    {
        MarioStateController m = (MarioStateController)controller;
        m.gameObject.GetComponent<AudioSource>().PlayOneShot(stateChangeStart);
        controller.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController;
    }
}