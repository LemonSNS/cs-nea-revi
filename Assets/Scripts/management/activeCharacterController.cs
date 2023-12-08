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


    
    void Start(){
        if (Instance != null){
            Debug.Log("More than one activeCharacterController active. What did you do??? Continued playing may break the game.");
        }
        Instance = this;
    }

    // Update is called once per frame
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
        
        if (allowActiveCharacterChange){
            if (Input.GetMouseButtonDown(0)){
                mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseGridPosition = activeCharacter.combatGrid.getGridPosition(mouseWorldPosition);
                tempCharacter = activeCharacter.combatGrid.getTile(mouseGridPosition).getCharacter();
                if (tempCharacter.playerTeam == true){
                    activeCharacter = tempCharacter;
                }
            }
        }
    }
}