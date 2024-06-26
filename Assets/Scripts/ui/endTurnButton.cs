using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endTurnButtonScript : MonoBehaviour
{

    public Button endTurnButton;

    void Awake() {
        combatManager.combatStateChanged.AddListener(combatPhase);
    }

    void OnDestroy() {
        combatManager.combatStateChanged.RemoveListener(combatPhase);
    }

    public void endTurn(){
        combatManager.Instance.updateCombatState(combatState.enemyTurn);
        combatManager.Instance.updateCombatState(combatState.playerTurn);
    }

    private void combatPhase(combatState phase){
        endTurnButton.interactable = phase == combatState.playerTurn;
    }
}
