using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transition1 : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        if (Weapon.instance.isAttacking){
            Weapon.instance.anim.Play("weapon_swing1");
            animator.SetTrigger("Swing");
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        Weapon.instance.isAttacking = false;
    }
}
