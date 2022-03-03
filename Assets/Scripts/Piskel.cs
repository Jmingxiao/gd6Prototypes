using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piskel : interactableObject
{
    Animator animator;
    bool onoff =false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame

    protected override void interact()
    {
        base.interact();
        onoff = !onoff;
        animator.SetBool("onoff",onoff);
    }
}
