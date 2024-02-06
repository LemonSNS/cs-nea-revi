using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damageTextController : MonoBehaviour
{
    public TMP_Text damageText;
    [SerializeField] private float fadeLength = 2.0f;
    private float fadeStart;
    public Vector3 initialPositon, finalPosition;
    public Color initialColor, finalColor; // 
    private float x;
    void Start(){
        fadeStart = Time.time;
    }

    void Update(){
        x = (Time.time-fadeStart)/fadeLength; //I don't know what to name this variable???
        if (x <=1){ //remember to report the crashes that were happening when you used a while lol
            damageText.color = Color.Lerp(initialColor, finalColor, x);
            transform.localPosition = Vector3.Lerp(initialPositon, finalPosition, x);
        }
        else{
            Destroy(gameObject);
        }
    }
}
