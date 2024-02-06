using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionManager : MonoBehaviour
{
    private List<Action> actionQueue = new List<Action>(); // I call it a queue, but it's not the CS textbook definition of a queue. 
    public Action currentAction;
    public static actionManager Instance;
    public static event Action<actionType> preCardInvoke;
    public static event Action<actionType> postCardInvoke;

    void Awake(){
        if (Instance != null){
            Debug.Log("More than one actionManager active. What did you do??? Continued playing may break the game.");
        }
        Instance = this;

        currentAction = new Action();
        //an empty action is produced to avoid null errors
    }

    void Update()
    {
        if (actionQueue.Count != 0 && currentAction.isDone){
            currentAction = nextAction();
        }
        else if (!currentAction.isDone){
            preCardInvoke?.Invoke(currentAction.actionType);
            currentAction.act();
            postCardInvoke?.Invoke(currentAction.actionType);
        }
    }

    public void addToBottom(List<Action> actionList){
        actionQueue.InsertRange(0, actionList);
    }

    public void addToTop(List<Action> actionList){
        actionQueue.AddRange(actionList); //I could just use the AddRange(action); whenever I need to
    } // but doing this allows me to add a bunch more clarity on what's going on.

    public void clearQueue(){
        actionQueue.Clear(); //similar reasoning to above.
    }

    public Action nextAction(){
        Action returnAction = actionQueue[0];
        actionQueue.RemoveAt(0);
        return returnAction;
    }
}