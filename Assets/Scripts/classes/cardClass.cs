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
        energyCostLabel.text = energyCost.ToString();
        momentumCostLabel.text = momentumCost.ToString();
    }

    void Update(){  //be sure to change this to event-based later!!
                    //constantly updating this is NOT good. 
        if (activeCharacterController.Instance.momentumActive){
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
        Debug.Log("You shouldn't be seeing this. You've selected a blank card.");
    }
    
    public virtual void cardPlayed(){
        Debug.Log("You shouldn't be seeing this. You've played a blank card.");
    }
}

public enum cardLocation{
    deck,
    hand,
    discardPile,
    removed
}
