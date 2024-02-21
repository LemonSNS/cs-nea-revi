using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action//the actionClass.cs file only exists for the sake of not having to repeat myself
{ //there's nothing planned for the action actionClass itself, other than to define more specific actions.
    public actionType actionType;
    public Character owner;
    public Character target;
    public bool isDone = false;
    public string name = "Empty Action [For testing purposes.]";

    public virtual void act(){
        Debug.Log("Something's gone HORRIBLY wrong if you're seeing this somehow.");
    }

    public override string ToString(){
        return name;
    }
}

public enum actionType{
    attack,
    block,
    draw,
    discard, //the four basics
    buff,
    debuff,
    cleanse,
    shuffle
    // add more actions as needed
}