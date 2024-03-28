using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardAction : Action
{
    public DrawCardAction(Character owner){
        this.owner = owner;
        this.actionType = actionType.draw;
        this.name = "Draw Card";
    }

    public override void act(){
        if (owner.deck.Count == 0){
            if (owner.discardPile.Count == 0){
                return;
            }
            actionManager.Instance.addToBottom(new List<Action>(){new DrawCardAction(owner)});
            actionManager.Instance.addToBottom(new List<Action>(){new ShuffleDiscardIntoDeckAction(owner)});
            return;
        }
        owner.deck[0].cardLocation = cardLocation.hand;
        owner.hand.Add(owner.deck[0]);
        owner.deck.Remove(owner.deck[0]);

    }
}
