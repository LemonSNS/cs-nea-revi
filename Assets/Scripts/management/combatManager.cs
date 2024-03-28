using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class combatEvent : UnityEvent<combatState> {}
public class combatManager : MonoBehaviour
{

    public static combatManager Instance;
    public combatState state;
    public static combatEvent combatStateChanged;
    public List<Character> allyTeam = new List<Character>();
    public List<Character> enemyTeam = new List<Character>();


    void Awake(){
        Instance = this;
        combatStateChanged = new combatEvent();
    }

    void Start(){
        updateCombatState(combatState.startCombat);
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