using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SetFloatBehaviour to klasa dziedzicz¹ca z StateMachineBehaviour, pozwala na automatyczne ustawianie wartoœci zmiennych typu float
// w animatorze Unity przy wchodzeniu lub wychodzeniu ze stanu lub maszyny stanów. U¿yteczna do kontrolowania parametrów animacji.

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool updateOnStateMachine, updateOnState;  // Ustawienie zmiennej przy wejœciu/wyjœciu ze stanu lub ca³ej maszyny stanów
    public float valueOnEnter, valueOnExit; // Wartoœci float ustawiane przy wejœciu i wyjœciu z wybranego stanu/maszyny stanów

    // Wywo³ywane przy wejœciu do stanu; jeœli updateOnState jest true, ustawia zmienn¹ float na valueOnEnter
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    // Wywo³ywane przy wyjœciu ze stanu; jeœli updateOnState jest true, ustawia zmienn¹ float na valueOnExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }

    // Wywo³ywane przy wejœciu do maszyny stanów; jeœli updateOnStateMachine jest true, ustawia zmienn¹ float na valueOnEnter
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    // Wywo³ywane przy wyjœciu z maszyny stanów; jeœli updateOnStateMachine jest true, ustawia zmienn¹ float na valueOnExit
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }
}
