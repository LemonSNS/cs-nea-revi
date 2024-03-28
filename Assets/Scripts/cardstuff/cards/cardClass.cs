using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    //the data that the player would probably like to know
    public Character owner;
    public int damage;
    public int energyCost=1;
    public int momentumCost=1;
    public bool upgraded = false;
    public string cardName = "Nihil";
    public string cardNameMomentum = "Nihilist";
    public string cardDescription = "This card is a test card, which uses the base Card class. It does absolutely nothing. Other than spend energy.";
    public string cardDescriptionMomentum = "The Nihilistic worldview saps your motivation and energy further. Why are you playing this card again?";
    public cardLocation cardLocation;
    //data and logic stuff
    public Character target;
    public List<Action> energyActionList = new List<Action>(); 
    public List<Action> momentumActionList = new List<Action>();
    //all of the components...
    public TMP_Text cardNameLabel;
    public TMP_Text cardDescriptionLabel;
    public TMP_Text energyCostLabel;
    public TMP_Text momentumCostLabel;
    public Image cardIconHolder;
    public string cardIconFileLocation = "skill_icon_skchr_ctable_1";
    public string cardIconFileLocationMomentum = "skill_icon_skchr_cutter_1";
    public List<Vector2> tileHighlightLocation = new List<Vector2>();
    
    void Start(){
        if (tileHighlightLocation.Count == 0){
            tileHighlightLocation.Add(new Vector2(0,0));
        }
        energyCostLabel.text = energyCost.ToString(); //setting the UI...
        momentumCostLabel.text = momentumCost.ToString();
        activeCharacterController.momentumStateChanged.AddListener(updateMomentumText); //keep up w momentum
        cardNameLabel.text = cardName; //the initial set, since it only updates when momentum is updated
        cardDescriptionLabel.text = cardDescription;
        cardIconHolder.sprite = Resources.Load<Sprite>(cardIconFileLocation);
    }

    private void updateMomentumText(bool momentumActive){
        if (momentumActive){
            cardNameLabel.text = cardNameMomentum;
            cardDescriptionLabel.text = cardDescriptionMomentum;
            cardIconHolder.sprite = Resources.Load<Sprite>(cardIconFileLocationMomentum);
        }
        else{
            cardNameLabel.text = cardName;
            cardDescriptionLabel.text = cardDescription;
            cardIconHolder.sprite = Resources.Load<Sprite>(cardIconFileLocation);
        }     
    }

    public void cardSelected(){
        
        if (owner.energy-energyCost >= 0 && activeCharacterController.Instance.activeCharacter == owner)
        {   
            if (activeCharacterController.Instance.momentumActive && owner.momentum-momentumCost<0){
                return;
            }
            else
            {
                if (activeCharacterController.Instance.activeCard == this){
                    Debug.Log("trigger");
                    cardPlayed();
                }
                else{
                    Debug.Log("trigger2");
                    activeCharacterController.Instance.setActiveCard(null);
                    activeCharacterController.Instance.setActiveCard(this);
                }
            } //player can switch the active card
        }   
    }

    public void cardPlayed(){
        owner.energy-= energyCost;
        if (activeCharacterController.Instance.momentumActive){
            owner.momentum -= momentumCost;
            actionManager.Instance.addToTop(momentumActionList);
        }
        else{
            actionManager.Instance.addToTop(energyActionList);
        }
        activeCharacterController.Instance.setActiveCard(null);
    }

    void OnMouseOver(){ //I could use onmouseenter, but that means that things might not
                        //work proeprly if the character moves whilst the card is hovered.
        foreach (Vector2 tileToHighlight in tileHighlightLocation){
            if (combatGridManager.Instance.combatGrid.getTile(owner.gridPosition + tileToHighlight) != null){
                combatGridManager.Instance.combatGrid.getTile(owner.gridPosition + tileToHighlight).targetedByAlly = true;
            }   
        }
    }

    void OnMouseExit(){
        foreach (Vector2 tileToHighlight in tileHighlightLocation){
            if (combatGridManager.Instance.combatGrid.getTile(owner.gridPosition + tileToHighlight) != null){
                combatGridManager.Instance.combatGrid.getTile(owner.gridPosition + tileToHighlight).targetedByAlly = false;
            }
        }
    }
}

public enum cardLocation{
    deck,
    hand,
    discardPile,
    removed
}