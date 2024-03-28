using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardHandAction : Action
{
    public DiscardHandAction(Character owner){
        this.owner = owner;
        this.actionType = actionType.discard;
        this.name = "Discarding hand";
    }

    public override void act(){
        foreach(Card card in owner.hand){
            card.cardLocation = cardLocation.discardPile;
        }
        owner.discardPile.AddRange(owner.hand);
        owner.hand.Clear();
    }
}