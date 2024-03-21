using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chuckRockAction : Action
{ //The most basic attack action I can think of at the moment. Might keep it in as a joke.
    private int damage = 1; //remember to make a calculateDamage globally accessible method at some point.
    public new actionType actionType = actionType.attack;
    
    public chuckRockAction(Character owner, int damage){
        this.owner = owner;
        this.damage = damage;
        name = "Chuck a Rock";
    }

    public override void act(){
        for (int i = Mathf.FloorToInt(owner.gridPosition.x); i < owner.combatGrid.width; i++){
            target = combatGridManager.Instance.getCharacterFromTile(new Vector2 (i, owner.gridPosition.y));
            //Debug.Log(target);
            if (target != null && target.playerTeam == false){
                target.takeDamage(damage);
                return;
            }
        }
    }
}
