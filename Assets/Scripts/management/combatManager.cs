using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatManager : MonoBehaviour
{

    public static combatManager Instance;
    public combatState state;
    public static event Action<combatState> combatStateChanged;

    void Awake(){
        Instance = this;
    }

    void Start(){
        updateCombatState(combatState.playerTurn);
    }


    public void updateCombatState(combatState newState){
        state = newState;

        switch(newState){
            case combatState.startCombat:
                break;
            case combatState.playerTurn:
                break;
            case combatState.enemyTurn:
                break;
            case combatState.endCombatLoss:
                break;
            case combatState.endCombatWin:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        combatStateChanged?.Invoke(newState);
    }
}

public enum combatState{
    startCombat,
    playerTurn,
    enemyTurn,
    endCombatWin,
    endCombatLoss,
}