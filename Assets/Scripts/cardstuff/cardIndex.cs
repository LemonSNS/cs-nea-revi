using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIndex : MonoBehaviour
{    
    public static Card emptyCard;
    public static Card ThrowKnifeCard;

    void Awake(){
        loadCard("emptyCard", ref emptyCard);
        loadCard("throwKnifeCard", ref ThrowKnifeCard);
        //add more cards as i continue to make the game.
    }

    private void loadCard(string prefabName, ref Card card){
        card = Resources.Load<Card>("cards/" + prefabName);
    }
}