using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    [SerializeField]Image godButton;
    void Start()
    {
        GameState.Instance.godMode = false;
    }
    public void GodModeActivate()
    {
        if(GameState.Instance.TurnGodMode())
            godButton.color = Color.green;
        else
            godButton.color = Color.red;
    }
}
