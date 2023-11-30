using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public actionType actionType;
    public Character owner;
    public Character target;
}

public enum actionType{
    attack,
    block,
    draw,
    discard, //the four basics
    buff,
    debuff
    // add more actions as needed
}