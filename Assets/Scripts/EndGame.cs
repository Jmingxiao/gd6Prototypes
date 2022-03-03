using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Update is called once per frame
    public SpriteRenderer Player;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            StartCoroutine(fade());
        }
    }

    IEnumerator fade(){
        float alpha = Player.color.a;
        while(alpha>0){
            alpha-= Time.deltaTime*2.0f;
            Player.color = new Color(0,0,0,alpha);
            yield return null;
        }
         UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
