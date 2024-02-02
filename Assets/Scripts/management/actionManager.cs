using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionManager : MonoBehaviour
{
    public List<Action> actionQueue = new List<Action>(); // I call it a queue, but it's not the CS textbook definition of a queue. 
    public Action currentAction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (actionQueue.Count != 0 && currentAction.isDone){
            //code goes here.
        }
    }
}
