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
    
    void Start(){
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

    public virtual void cardSelected(){
        if (owner.energy-energyCost >= 0 && activeCharacterController.Instance.activeCharacter == owner){
            if (activeCharacterController.Instance.activeCard = this){
                activeCharacterController.Instance.activeCard = null; 
            } //player clicks on card again to deselect
            else{
                activeCharacterController.Instance.activeCard = this;
            } //player can switch the active card
        }   
    }
    
    public virtual void cardPlayed(){
        if (activeCharacterController.Instance.momentumActive){
            actionManager.Instance.addToTop(momentumActionList);
        }
        else{
            actionManager.Instance.addToTop(energyActionList);
        }
    }
}

public enum cardLocation{
    deck,
    hand,
    discardPile,
    removed
}
