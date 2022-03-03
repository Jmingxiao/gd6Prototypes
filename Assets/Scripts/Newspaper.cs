using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : interactableObject
{
    [SerializeField]SpriteRenderer papersprite; 
    public List<Sprite> papers;
    int papernum;
    // Update is called once per frame
    protected override void interact()
    {
        base.interact();
        papernum++;
        papernum %= papers.Count;
        papersprite.sprite = papers[papernum];
    }

}
