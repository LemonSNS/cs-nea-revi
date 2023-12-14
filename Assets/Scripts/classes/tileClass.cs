using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Tile : MonoBehaviour
{
    private Grid gridSystem;
    private Vector2 gridPosition;
    private GameObject tile;
    private Character character;
    private TMP_Text debugDisplay;
    private SpriteRenderer renderer;
    private Color activeCharacterColor = new Color (0f, 0.3f, 1f, 1f);
    private Color enemyCharacterColor = new Color (1f, 0f, 0f, 1f);
    private Color allyCharacterColor = new Color(0f, 1f, 0f, 1f);
    private Color nullColor = new Color(1f, 1f, 1f, 1f);

    public Tile(Grid gridSystem, Vector2 gridPosition, GameObject tile){
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        this.tile = tile;
        this.renderer = tile.GetComponent<SpriteRenderer>();
    }

    public Character getCharacter(){
        return character;
    }

    public void setCharacter(Character character){
        Debug.Log(character);
        this.character = character;
        if (character == null){
            Debug.Log("Successfully nulled.");
            renderer.color = nullColor;
        }
        else if (character == activeCharacterController.Instance.activeCharacter){
            renderer.color = activeCharacterColor;
        }
        else if (character.playerTeam){
            renderer.color = allyCharacterColor;
        }
        else if (character.playerTeam == false){
            renderer.color = enemyCharacterColor;
        }
    }
}
