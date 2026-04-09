using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine 
{
    BossState currentState;

    public BossStateMachine(BossState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(BossState newState)
    {
        currentState.Leave();
        currentState = newState;
        currentState.Enter();
    }

    public BossState getCurrentState()
    {
        return currentState;
    }
}
