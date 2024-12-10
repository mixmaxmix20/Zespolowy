using UnityEngine;

// SetBoolBehaviour to klasa dziedzicz¹ca z StateMachineBehaviour, pozwala na automatyczne ustawianie wartoœci zmiennych typu bool
// w animatorze Unity przy wchodzeniu lub wychodzeniu ze stanu lub maszyny stanów. U¿yteczna do kontrolowania warunków w animacjach.

public class SetBoolBehaviour : StateMachineBehaviour
{
    public string boolName; 
    public bool updateOnStateMachine, updateOnState;  // Ustawienie zmiennej przy wejœciu/wyjœciu ze stanu lub ca³ej maszyny stanów
    public bool valueOnEnter, valueOnExit; // Wartoœci bool ustawiane przy wejœciu i wyjœciu z wybranego stanu/maszyny stanów

    // Wywo³ywane przy wejœciu do stanu; jeœli updateOnState jest true, ustawia zmienn¹ bool na valueOnEnter
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    // Wywo³ywane przy wyjœciu ze stanu; jeœli updateOnState jest true, ustawia zmienn¹ bool na valueOnExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }

    // Wywo³ywane przy wejœciu do maszyny stanów; jeœli updateOnStateMachine jest true, ustawia zmienn¹ bool na valueOnEnter
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    // Wywo³ywane przy wyjœciu z maszyny stanów; jeœli updateOnStateMachine jest true, ustawia zmienn¹ bool na valueOnExit
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }
}
