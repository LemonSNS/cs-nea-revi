using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleDiscardIntoDeckAction : Action
{
    private Card tempCard;
    private int shuffle2;

    public ShuffleDiscardIntoDeckAction(Character owner){
        this.owner = owner;
        this.actionType = actionType.shuffle;
        this.name = "Shuffling/Refilling deck";
    }

    public override void act(){
        //shuffle1 and shuffle2 are just the selected cards to be shuffled.
        //basically the first card in the list switches places with a random card in the rest
        //then the second with any cards in index 2 or later, etc...
        for (int shuffle1=0; shuffle1<owner.discardPile.Count-1; shuffle1++){
            shuffle2 = Random.Range(shuffle1, owner.discardPile.Count);
            tempCard = owner.discardPile[shuffle1];                //common, useful shuffle.
            owner.discardPile[shuffle1] = owner.discardPile[shuffle2];
            owner.discardPile[shuffle2] = tempCard;
        }

        foreach (Card card in owner.discardPile){
            card.cardLocation = cardLocation.deck;
        }
        owner.deck.AddRange(owner.discardPile);
        owner.discardPile.Clear();
    }
}
