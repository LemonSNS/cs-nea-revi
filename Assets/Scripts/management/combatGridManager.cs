using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class characterMoveEvent : UnityEvent<string>{}
//There's no parameter actually needed for this, at the moment, so I've set it to string for now.
public class combatGridManager : MonoBehaviour
{

    [SerializeField] private GameObject tilePrefab;
    public Grid combatGrid;
    public static combatGridManager Instance;
    public static characterMoveEvent characterHasMoved;

    void Awake(){
        if (Instance != null){
            Debug.LogError("More than one combat Grid.");
        }
        Instance = this;
        combatGrid = new Grid(13, 6, 1, new Vector3(2f, -0.5f, 0f), tilePrefab);
        characterHasMoved = new characterMoveEvent();
    }

    public void setCharacterToTile(Vector2 gridPosition,Character character){
        Tile tile = combatGrid.getTile(gridPosition);
        tile.setCharacter(character);
    }

    public Character getCharacterFromTile(Vector2 gridPosition){
        return combatGrid.getTile(gridPosition).getCharacter();
    }

    public void clearCharacterFromTile(Vector2 gridPosition){
        combatGrid.getTile(gridPosition).setCharacter(null);
    }

    public Vector2 getGridPosition(Vector3 worldPosition){
        return combatGrid.getGridPosition(worldPosition);
    }

    public void moveCharacter(Vector2 initialGridPosition, Vector2 finalGridPosition, Character character){
        clearCharacterFromTile(initialGridPosition);
        setCharacterToTile(finalGridPosition, character);
        characterHasMoved?.Invoke("");
        activeCharacterController.Instance.setActiveCard(null);
    }
}