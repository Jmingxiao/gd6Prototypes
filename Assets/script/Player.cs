using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector]public Vector3 target;
    [HideInInspector]public int currentTask=0;
    [HideInInspector]public float bubboTimer= 0.0f;
    public List<Task> tasks;
    public State playerstate
    {
        get { return currentState; }
    }
    private State currentState;
    public readonly IdleState idle = new IdleState();
    public readonly Walkstate walk = new Walkstate();
    public readonly ProcessState process = new ProcessState();
    
    private void Start() {
        foreach(var t in tasks){
            t.init();
        }
        TransitionToState(idle);
        bubboTimer =Config.bubbointer;
    }
    private void Update() {
       currentState.Update(this);       
    }

    public void TransitionToState(State state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    public bool bubboUpdate(){
        tasks[currentTask].onBubbo(transform);
        if(tasks[currentTask].OnClickBubbo()){
            bubboTimer=0.0f;
            return true;
        }
        return false;
    }
    public void SetTarget(){
        target = tasks[currentTask].targetDestination.position;
    }
    public bool MovetoTarget(){
        var des = new Vector3(target.x,transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position,des,Time.deltaTime*3.0f);
        return (transform.position-des).magnitude<=0.1f;
    }
    public void switchTask(){
        GetCurrentTask().completeTimes++;
        currentTask = (currentTask+1)%tasks.Count;
    }
    public Task GetCurrentTask(){
        if(tasks.Count>currentTask){
            return tasks[currentTask];
        }
        return null;
    }
}
