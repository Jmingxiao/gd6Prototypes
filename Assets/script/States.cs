using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
   public abstract void EnterState(Player player);

   public abstract void Update(Player player);

   public abstract void OnExit(Player player); 
}


public class IdleState: State{
    
    public override void EnterState(Player player)
    {   
       if(player.GetCurrentTask().completeTimes==0){
           player.bubboTimer= Config.bubbointer;
       }
        Debug.Log("IDLE");
    }
    public override void Update(Player player)
    {
        player.bubboTimer+=Time.deltaTime;
        if(player.bubboTimer>Config.bubbointer){
            if(player.bubboUpdate()){
                OnExit(player);
            }
        }
    }
    public override void OnExit(Player player)
    {
        player.TransitionToState(player.walk);
    }
}

public class Walkstate:State{
    public override void EnterState(Player player)
    {
        player.SetTarget();
        Debug.Log("walk");
    }
    public override void Update(Player player)
    {
       if( player.MovetoTarget()){
           OnExit(player);
       }
        
    }

    public override void OnExit(Player player)
    {
       player.TransitionToState(player.process);
    }
}

public class ProcessState:State{
    float timer =0.0f;
    float processInter =2.0f;
    public override void EnterState(Player player)
    {
        Debug.Log("process");
        timer = 0.0f;
    }
    public override void Update(Player player)
    {
        timer+= Time.deltaTime;

        if(timer>=processInter){
            OnExit(player);
        }
    }

    public override void OnExit(Player player)
    {
        player.switchTask();
        player.TransitionToState(player.idle);
    }
}