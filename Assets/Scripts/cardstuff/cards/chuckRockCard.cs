using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chuckRockCard : Card
{
    void Awake()
    {
        energyCost = 1;
        momentumCost = 2;
        energyActionList.Add(new chuckRockAction(owner, damage));
        momentumActionList.Add(new chuckRockAction(owner, damage + 300));
        cardName = "Hunt";
        cardNameMomentum = "Hunter's Pursuit";
        cardDescription = $"A throwing knife used by hunters. Deal {damage} damage in a straight ahead of you.";
        cardDescriptionMomentum = $"A hunter knows their target's weak spots. Deal {damage + 300} instead.";
        cardIconFileLocation = "skill_icon_skchr_ctable_1";
        cardIconFileLocationMomentum = "skill_icon_skchr_cutter_1";
    }

    public override void cardSelected()
    {
        if (owner.energy-energyCost >= 0 && activeCharacterController.Instance.activeCharacter == owner){
            if (activeCharacterController.Instance.activeCard = this){
                activeCharacterController.Instance.activeCard = null; 
            } //player clicks on card again to deselect
            else{
                activeCharacterController.Instance.activeCard = this;
            } //player can switch the active card
        }
    }

    public override void cardPlayed()
    {
        if (activeCharacterController.Instance.momentumActive){
            actionManager.Instance.addToTop(momentumActionList);
        }
        else{
            actionManager.Instance.addToTop(energyActionList);
        }
    }
}
