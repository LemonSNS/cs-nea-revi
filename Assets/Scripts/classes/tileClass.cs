using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Tile
{
    private Grid gridSystem;
    private Vector2 gridPosition;
    private GameObject tile;
    private Character character;
    private TMP_Text debugDisplay;

    public Tile(Grid gridSystem, Vector2 gridPosition, GameObject tile){
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        this.tile = tile;
    }

    public Character getCharacter(){
        return character;
    }

    public void setCharacter(Character character){
        this.character = character;
    }
}
