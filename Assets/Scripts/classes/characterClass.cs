using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int momentum;
    public int maxMomentum;
    public int energy;
    public int maxEnergy;
    [SerializeField] private TMP_Text healthCounter;
    [SerializeField] private TMP_Text momentumCounter;
    [SerializeField] private TMP_Text energyCounter;
    private float moveSpeed = 12f;
    public Transform movePoint;
    public int characterID = 0;
    public Vector2 gridPosition;
    public Grid combatGrid;
    public bool playerTeam;
    public Dictionary<string, dynamic> statusEffects;
    public List<Card> deck = new List<Card>();          //because it'll be a massive hassle if i only have
    public List<Card> combatDeck = new List<Card>();    //one deck attribute to do all the logic in
    public List<Card> hand = new List<Card>();          //since I have to do in combat logic and out of combat logic
    public List<Card> discardPile = new List<Card>();
    public List<Card> exhaustPile = new List<Card>();
    [SerializeField] private damageTextController damagePopupCanvas;
    private damageTextController newDamagePopupCanvas;
    private Card tempCard; //just a holder, really.
    [SerializeField] private Transform deckHolder; //exists so that the hierarchy doesn't flood, and I can actually access them.

    void Awake(){
        combatManager.combatStateChanged.AddListener(combatPhaseChanged);
    }

    void OnDestroy() {
        combatManager.combatStateChanged.RemoveListener(combatPhaseChanged);
    }

    void Start()
    {
        movePoint.parent = null;
        combatGrid = combatGridManager.Instance.combatGrid;
        gridPosition = combatGrid.getGridPosition(transform.position);
        combatGridManager.Instance.setCharacterToTile(gridPosition, this);
        setBasicDeck(); //might look a little bloated if I just put the entire thing in here.
        deckHolder.parent = null;
    }

    private void setBasicDeck(){ //just how the game's intended to be, really.
        if (playerTeam){
        for (int i=0; i<8; i++){ //who knows how i'll deal with a basic deck down the line?
            addToDeck(ref CardIndex.ThrowKnifeCard);
        }
        for (int i=0; i<5; i++){
            addToDeck(ref CardIndex.emptyCard);
        }}
    }

    // Update is called once per frame
    void Update()
    {
        healthCounter.text = $"{health}";
        momentumCounter.text = $"{momentum}";
        energyCounter.text = $"{energy}";
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed*Time.deltaTime);
    }

    private void combatPhaseChanged(combatState phase){
        if (phase == combatState.playerTurn){
            energy = maxEnergy;
            if (momentum != maxMomentum){
                momentum++;
            }
            for (int i=0; i<5; i++){
                actionManager.Instance.addToTop(new List<Action>(){new DrawCardAction(this)});
            }
        }

        if (phase == combatState.enemyTurn){
            actionManager.Instance.addToBottom(new List<Action>(){new DiscardHandAction(this)});
        }

        if (phase == combatState.startCombat){
            discardPile = new List<Card>(deck);
        }   //because when turn starts the draw action means discard pile's gonna be shuffled into combatDeck, this will work just fine.
    }

    public void takeDamage(int damage){
        health -= damage;
        newDamagePopupCanvas = Instantiate(damagePopupCanvas, this.transform.position, Quaternion.identity, this.transform);
        newDamagePopupCanvas.damageText.text = $"-{damage}";
        Debug.Log("Damage done");
    }
    
    public void addToDeck(ref Card card){   //more general utility methods~~~~~~~~~~~~~~~~~~~~~~~~~~ woo.
        tempCard = Instantiate(card);
        deck.Add(tempCard);
        tempCard.owner = this;
        tempCard.gameObject.SetActive(false);       //again, gotta make sure not to bloat the hierarchy...
        tempCard.transform.SetParent(deckHolder);   //though, I've kinda already done that with tiles...   
    }                                               //but to be fair, tiles aren't as complicated.
}
