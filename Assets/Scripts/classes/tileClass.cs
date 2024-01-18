using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Tile : MonoBehaviour
{
    private Grid gridSystem;
    private Vector2 gridPosition;
    [SerializeField] private Character character;
    [SerializeField] new private SpriteRenderer renderer;
    private static Color activeCharacterColor = new Color (0f, 0.3f, 1f);
    private static Color enemyCharacterColor = new Color (1f, 0f, 0f);
    private static Color allyCharacterColor = new Color(0f, 1f, 0f);
    private static Color nullColor = new Color(1f, 1f, 1f);

    public Tile(Grid gridSystem, Vector2 gridPosition){
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public Character getCharacter(){
        return character;
    }

    public void setCharacter(Character character){
        Debug.Log(character);
        this.character = character;
    }

    void Update()
    {
        if (character == null){
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