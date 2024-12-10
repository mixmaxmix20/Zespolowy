using UnityEngine;

// SetBoolBehaviour to klasa dziedzicz�ca z StateMachineBehaviour, pozwala na automatyczne ustawianie warto�ci zmiennych typu bool
// w animatorze Unity przy wchodzeniu lub wychodzeniu ze stanu lub maszyny stan�w. U�yteczna do kontrolowania warunk�w w animacjach.

public class SetBoolBehaviour : StateMachineBehaviour
{
    public string boolName; 
    public bool updateOnStateMachine, updateOnState;  // Ustawienie zmiennej przy wej�ciu/wyj�ciu ze stanu lub ca�ej maszyny stan�w
    public bool valueOnEnter, valueOnExit; // Warto�ci bool ustawiane przy wej�ciu i wyj�ciu z wybranego stanu/maszyny stan�w

    // Wywo�ywane przy wej�ciu do stanu; je�li updateOnState jest true, ustawia zmienn� bool na valueOnEnter
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    // Wywo�ywane przy wyj�ciu ze stanu; je�li updateOnState jest true, ustawia zmienn� bool na valueOnExit
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }

    // Wywo�ywane przy wej�ciu do maszyny stan�w; je�li updateOnStateMachine jest true, ustawia zmienn� bool na valueOnEnter
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    // Wywo�ywane przy wyj�ciu z maszyny stan�w; je�li updateOnStateMachine jest true, ustawia zmienn� bool na valueOnExit
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }
}
