using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    EnemyState currentState;

    public EnemyStateMachine(EnemyState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.Leave();
        currentState = newState;
        currentState.Enter();
    }

    public EnemyState getCurrentState()
    {
        return currentState;
    }
}
