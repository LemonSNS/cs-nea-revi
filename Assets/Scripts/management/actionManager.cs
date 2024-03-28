using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class actionEvent : UnityEvent<actionType>{}
public class actionManager : MonoBehaviour
{
    public static actionEvent preActionInvoke;
    public static actionEvent postActionInvoke;
    public List<Action> actionQueue = new List<Action>(); // I call it a queue, but it's not the CS textbook definition of a queue. 
    public Action currentAction;
    public static actionManager Instance;
    private bool resolving; 

    void Awake(){
        if (Instance != null){
            Debug.Log("More than one actionManager active. What did you do??? Continued playing may break the game.");
        }
        Instance = this;
        currentAction = new Action();
        currentAction.isDone = true;
        //an empty action is produced at the start to avoid null errors
    }

    void Update()
    {
        if (!resolving){
            if (actionQueue.Count != 0 && currentAction.isDone){
                currentAction = nextAction();
                currentAction.isDone = false;
            }
            else if (!currentAction.isDone){
                StartCoroutine(resolveAction());
            }
            // the conditions are intentionally non-exhaustive, because
        }   //otherwise we'd have an underflow/null/index error in nextAction(); 
    }

    private IEnumerator resolveAction(){
        resolving = true;
        preActionInvoke?.Invoke(currentAction.actionType);
        currentAction.act();
        postActionInvoke?.Invoke(currentAction.actionType);
        yield return new WaitForSeconds(0.1f);
        resolving = false;
        currentAction.isDone = true;
    }

    public void addToBottom(List<Action> actionList){
        actionQueue.InsertRange(0, actionList);
        string debugString = "[";
        for (int i = 0; i<actionQueue.Count; i++){
            debugString += actionQueue[i].ToString();
            if (i != actionQueue.Count-1){debugString += ", ";}
            else {debugString += "]";}
        }
        Debug.Log(debugString);
    }
    public void addToTop(List<Action> actionList){
        string debugString = "[";
        actionQueue.AddRange(actionList);
        for (int i = 0; i<actionQueue.Count; i++){
            debugString += actionQueue[i].ToString();
            if (i != actionQueue.Count-1){debugString += ", ";}
            else {debugString += "]";}
        }
        Debug.Log(debugString);
        //I could just use the AddRange(action); whenever I need to but
    }   //doing this allows me to add a bunch more clarity on what's going on.

    public void clearQueue(){
        actionQueue.Clear(); //similar reasoning to above.
    }

    public Action nextAction(){
        Action returnAction = actionQueue[0];
        actionQueue.RemoveAt(0);
        return returnAction;
    }
}