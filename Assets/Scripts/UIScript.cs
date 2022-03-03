using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript: MonoBehaviour
{
    [SerializeField]Text text;
    [SerializeField]GameObject cover;
    // Start is called before the first frame update
    private void Awake() {
        cover.SetActive(false);
    }

    public void Finish(string score){
        cover.SetActive(true);
        text.text = "You lose!!! Your score is: " + score; 
    }
    public void replay(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void quit(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
