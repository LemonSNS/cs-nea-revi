using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Tile : MonoBehaviour
{
    private Grid gridSystem;
    private Vector2 gridPosition;
    [SerializeField] private Character character;
    [SerializeField] private new SpriteRenderer renderer;
    private static Color activeCharacterColor = new Color (0f, 0.3f, 1f);
    private static Color enemyCharacterColor = new Color (1f, 0f, 0f);
    private static Color allyCharacterColor = new Color(0f, 1f, 0f);
    private static Color nullColor = new Color(1f, 1f, 1f);
    public bool targetedByAlly;
    public bool targetedByEnemy;
    [SerializeField] private SpriteRenderer targetingRenderer;

    public Tile(Grid gridSystem, Vector2 gridPosition){
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    void Start(){
        combatGridManager.characterHasMoved.AddListener(refreshAllyTargeting);
    }

    void OnDestroy(){
        combatGridManager.characterHasMoved.RemoveListener(refreshAllyTargeting);
    }

    public Character getCharacter(){
        return character;
    }

    public void setCharacter(Character character){
        this.character = character;
    }

    public void refreshAllyTargeting(string dummy){
        targetedByAlly = false;
    }

    public void refreshEnemyTargeting(string dummy){
        targetedByEnemy = false;
    }

    void Update()
    {
        if (activeCharacterController.Instance.activeCard == null){
            if (targetedByAlly && targetedByEnemy){
                targetingRenderer.color = new Color(1f, 0f, 1f, 1f);
            }
            else if (targetedByAlly){
                targetingRenderer.color = activeCharacterColor;
            }
            else if (targetedByEnemy){
                targetingRenderer.color = enemyCharacterColor;
            }
            else{
                targetingRenderer.color = new Color(0f,0f,0f,0f);
            }
        }
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