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

    public void cardPlayed(){
        if (owner.energy > 0 && activeCharacterController.Instance.activeCharacter == owner){
            for (int i = Mathf.FloorToInt(owner.gridPosition.x); i < owner.combatGrid.width; i++){
                target = combatGridManager.Instance.getCharacterFromTile(new Vector2 (i, owner.gridPosition.y));
                Debug.Log(target);
                if (target != null && target.playerTeam == false){
                    target.takeDamage(damage);
                }
            }
            owner.energy -= cost;
        }
    }
    public void OnMouseOver(){
        
    }
}

public enum cardLocation{
    deck,
    hand,
    discardPile,
    removed
}
