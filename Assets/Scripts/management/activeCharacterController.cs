using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeCharacterController : MonoBehaviour 
{
    public Character activeCharacter;
    public static activeCharacterController Instance;
    public bool allowActiveCharacterChange;
    private Vector2 initialGridPosition;
    private Vector2 finalGridPosition;
    private Vector3 mouseWorldPosition;
    private Vector3 mouseGridPosition;
    private Character tempCharacter;
    public Card activeCard;
    public bool momentumActive = false;
    
    void Awake(){
        if (Instance != null){
            Debug.Log("More than one activeCharacterController active. What did you do??? Continued playing may break the game.");
        }
        Instance = this;
    }

    void Update()
    {
        
        if (Vector3.Distance(activeCharacter.transform.position, activeCharacter.movePoint.position) == 0f && activeCharacter.energy != 0){
                initialGridPosition = activeCharacter.gridPosition;
                if(Input.GetKeyDown(KeyCode.W) && activeCharacter.gridPosition.y+1 < activeCharacter.combatGrid.height && combatGridManager.Instance.getCharacterFromTile(activeCharacter.gridPosition + new Vector2 (0, 1)) == null){
                    activeCharacter.movePoint.position += new Vector3(0, 1, 0);
                    activeCharacter.gridPosition.y++;
                    activeCharacter.energy--;
                }
                else if(Input.GetKeyDown(KeyCode.A) && activeCharacter.gridPosition.x > 0 && combatGridManager.Instance.getCharacterFromTile(activeCharacter.gridPosition + new Vector2 (-1, 0)) == null){
                    activeCharacter.movePoint.position += new Vector3(-1, 0, 0);
                    activeCharacter.gridPosition.x--;
                    activeCharacter.energy--;
                }

                else if(Input.GetKeyDown(KeyCode.S) && activeCharacter.gridPosition.y > 0 && combatGridManager.Instance.getCharacterFromTile(activeCharacter.gridPosition + new Vector2 (0, -1)) == null){
                    activeCharacter.movePoint.position += new Vector3(0, -1, 0);
                    activeCharacter.gridPosition.y--;
                    activeCharacter.energy--;
                }

                else if(Input.GetKeyDown(KeyCode.D) && activeCharacter.gridPosition.x+1 < activeCharacter.combatGrid.width && combatGridManager.Instance.getCharacterFromTile(activeCharacter.gridPosition + new Vector2 (1, 0)) == null){
                    activeCharacter.movePoint.position += new Vector3(1, 0, 0);
                    activeCharacter.gridPosition.x++;
                    activeCharacter.energy--;
                }
                finalGridPosition = activeCharacter.gridPosition;
                if (initialGridPosition != finalGridPosition){
                    combatGridManager.Instance.moveCharacter(initialGridPosition, finalGridPosition, activeCharacter);
                }
            }
        
        if (allowActiveCharacterChange){    //because allowActiveCharacterChange is generally on, sometimes an error will be reported if you click outside the grid.
                                            //This doesn't impair the functionality of the game, yet. 12/12/23
            if (Input.GetMouseButtonDown(0)){ // note to self: the editor reports an error if you click on an empty tile, be cause the tile contains "null" character.
                mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseGridPosition = activeCharacter.combatGrid.getGridPosition(mouseWorldPosition);
                if (mouseGridPosition.x < 0 || mouseGridPosition.y < 0 || mouseGridPosition.x >= activeCharacter.combatGrid.width || mouseGridPosition.y >= activeCharacter.combatGrid.height){
                    Debug.Log("Invalid Grid Position on mouse.");
                    return;
                }
                tempCharacter = activeCharacter.combatGrid.getTile(mouseGridPosition).getCharacter();
                if (tempCharacter == null){
                    Debug.Log("Empty Tile.");
                }
                else if (tempCharacter.playerTeam == true){
                    Debug.Log("activeCharacter updated.");
                    activeCharacter = tempCharacter;
                }
            }
        }
    }

    public void setActiveCard(Card activeCard){
        this.activeCard = activeCard;
    }
}
