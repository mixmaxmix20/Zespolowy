using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SetFloatBehaviour to klasa dziedzicz�ca z StateMachineBehaviour, pozwala na automatyczne ustawianie warto�ci zmiennych typu float
// w animatorze Unity przy wchodzeniu lub wychodzeniu ze stanu lub maszyny stan�w. U�yteczna do kontrolowania parametr�w animacji.

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool updateOnStateMachine, updateOnState;  // Ustawienie zmiennej przy wej�ciu/wyj�ciu ze stanu lub ca�ej maszyny stan�w
    public float valueOnEnter, valueOnExit; // Warto�ci float ustawiane przy wej�ciu i wyj�ciu z wybranego stanu/maszyny stan�w

    // Wywo�ywane przy wej�ciu do stanu; je�li updateOnState jest true, ustawia zmienn� float na valueOnEnter
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    // Wywo�ywane przy wyj�ciu ze stanu; je�li updateOnState jest true, ustawia zmienn� float na valueOnExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }

    // Wywo�ywane przy wej�ciu do maszyny stan�w; je�li updateOnStateMachine jest true, ustawia zmienn� float na valueOnEnter
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    // Wywo�ywane przy wyj�ciu z maszyny stan�w; je�li updateOnStateMachine jest true, ustawia zmienn� float na valueOnExit
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }
}
