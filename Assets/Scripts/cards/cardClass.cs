using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Character owner;
    public int damage;
    public int cost;
    public Character target;
    public cardLocation cardLocation;
    public int upgradeTier = 0;
    [SerializeField] public List<Action> actionList = new List<Action>(); //glad to remember lists are mostly what I need here.

    void Awake(){
        actionList.Add(new chuckRockAction(owner, damage));
        Debug.Log(actionQueue);
    }
    public void cardPlayed(){
        if (owner.energy > 0 && activeCharacterController.Instance.activeCharacter == owner){
            owner.energy -= cost;
            //add to actionqueue or whatever in the manager later. First figure out if acitons work.
            actionList[0].act();
        }
    }
}

public enum cardLocation{
    deck,
    hand,
    discardPile,
    removed
}
