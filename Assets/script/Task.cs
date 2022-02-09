using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task 
{
    public GameObject bubboSprite;
    [HideInInspector]public int completeTimes = 0;
    public Transform targetDestination;

     bool hasBubbo =false;

    public void init(){
        hasBubbo =false;
    }
    public void onBubbo(Transform playerPos){
        if(!hasBubbo){
           bubboSprite =GameObject.Instantiate(bubboSprite);
           bubboSprite.transform.parent = playerPos;
           bubboSprite.transform.localPosition =Config.Bboffset; 
           hasBubbo=true;
        }else if(!bubboSprite.activeSelf){
            bubboSprite.SetActive(true);
        }
    }
    public bool OnClickBubbo(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider!=null&&hit.collider.CompareTag("Bubbo") ) {
                bubboSprite.SetActive(false);
                return true;
            }
        }
        return false;
    }
   
}
