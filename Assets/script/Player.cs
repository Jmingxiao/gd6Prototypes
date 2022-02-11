using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector]public Vector3 target;
    [HideInInspector]public int currentTask=0;
    [HideInInspector]public float bubboTimer= 0.0f;
    [HideInInspector]public bool firstround=true;
    [HideInInspector]public Animator animator;

    [SerializeField] GameObject waterbottle;
    [SerializeField] GameObject Key;

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
        animator = GetComponent<Animator>();
        Key.SetActive(false);
        waterbottle.SetActive(false);
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
        transform.eulerAngles=target.x-transform.position.x<0?new Vector3(0,180,0):Vector3.zero;
        animator.SetBool("walk",true);
    }
    public bool MovetoTarget(){
        var des = new Vector3(target.x,transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position,des,Time.deltaTime*3.0f);
        return (transform.position-des).magnitude<=0.1f;
    }
    public void switchTask(){
        if(!firstround&&tasks.Count>=3){
            currentTask += Random.Range(1,tasks.Count-1);
        }else{
            if(tasks.Count-1==currentTask){
                firstround=false;
            }
            GetCurrentTask().completeTimes++;
            currentTask = (currentTask+1)%tasks.Count;
        }
    }
    public Task GetCurrentTask(){
        
        if(tasks.Count>currentTask){
            return tasks[currentTask];
        }
        return null;
    }

    public void TaskUpdate(bool start){
          switch(GetCurrentTask().name){
            case "waterPlant":
                waterbottle.SetActive(start);
            break;
            case "lockDoor":
                Key.SetActive(start);
            break;
            default:
            break;
        }
    }
}
