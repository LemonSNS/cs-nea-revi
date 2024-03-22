using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnifeCard : Card
{
    void Awake()
    {
        damage = 100;
        energyCost = 1;
        momentumCost = 2;
        energyActionList.Add(new ThrowKnifeAction(owner, damage));
        momentumActionList.Add(new ThrowKnifeAction(owner, damage + 300));
        cardName = "Hunt";
        cardNameMomentum = "Hunter's Pursuit";
        cardDescription = $"A throwing knife used by hunters. Deal {damage} damage to the first target ahead of you.";
        cardDescriptionMomentum = $"A hunter knows their target's weak spots. Deal {damage + 300} instead.";
        cardIconFileLocation = "skill_icon_skchr_ctable_1";
        cardIconFileLocationMomentum = "skill_icon_skchr_cutter_1";
    }
}
