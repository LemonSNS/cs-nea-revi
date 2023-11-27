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
    [SerializeField] private Transform movePoint;
    public int characterID = 0;
    public Vector2 gridPosition;
    private Vector2 initialGridPosition;
    private Vector2 finalGridPosition;
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
        
        if (playerTeam) {
            if (Vector3.Distance(transform.position, movePoint.position) == 0f && energy != 0){
                initialGridPosition = gridPosition;
                if(Input.GetKeyDown(KeyCode.W) && gridPosition.y+1 < combatGrid.height && combatGridManager.Instance.getCharacterFromTile(gridPosition + new Vector2 (0, 1)) == null){
                    movePoint.position += new Vector3(0, 1, 0);
                    gridPosition.y++;
                    energy--;
                }
                else if(Input.GetKeyDown(KeyCode.A) && gridPosition.x > 0 && combatGridManager.Instance.getCharacterFromTile(gridPosition + new Vector2 (-1, 0)) == null){
                    movePoint.position += new Vector3(-1, 0, 0);
                    gridPosition.x--;
                    energy--;
                }

                else if(Input.GetKeyDown(KeyCode.S) && gridPosition.y > 0 && combatGridManager.Instance.getCharacterFromTile(gridPosition + new Vector2 (0, -1)) == null){
                    movePoint.position += new Vector3(0, -1, 0);
                    gridPosition.y--;
                    energy--;
                }

                else if(Input.GetKeyDown(KeyCode.D) && gridPosition.x+1 < combatGrid.width&& combatGridManager.Instance.getCharacterFromTile(gridPosition + new Vector2 (1, 0)) == null){
                    movePoint.position += new Vector3(1, 0, 0);
                    gridPosition.x++;
                    energy--;
                }
                finalGridPosition = gridPosition;
                if (initialGridPosition != finalGridPosition){
                    combatGridManager.Instance.moveCharacter(initialGridPosition, finalGridPosition, this);
                }
            }
        }

        else{
            //enemy code goes here
        }
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
