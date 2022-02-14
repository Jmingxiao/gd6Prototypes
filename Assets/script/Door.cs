using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    SpriteRenderer sprite;
    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }
    
    public void Opendoor(){
        sprite.color = new Color(0,0,0,1);
    }

    public bool onClick(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider!=null&&hit.collider.CompareTag("Door")) {
                return true;
            }
        }
        return false;
    }
}
