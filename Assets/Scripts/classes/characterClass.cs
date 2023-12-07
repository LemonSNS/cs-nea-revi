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
    private float moveSpeed = 5f;
    public Transform movePoint;
    public int characterID = 0;
    public Vector2 gridPosition;
    public Grid combatGrid;
    public bool playerTeam;
    public Dictionary<string, dynamic> statusEffects;
    public Card[] deck;

    void Awake(){
        combatManager.combatStateChanged += combatPhaseChanged;
    }

    void OnDestroy() {
        combatManager.combatStateChanged -= combatPhaseChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        combatGrid = combatGridManager.Instance.combatGrid;
        gridPosition = combatGrid.getGridPosition(transform.position);
        combatGridManager.Instance.setCharacterToTile(gridPosition, this);


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
        }
    }

    public void takeDamage(int damage){
        health -= damage;
        Debug.Log("Damage done");
    }
}
