using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public UnityEngine.UI.Text time;
    public  GameObject phone;
    private void Start() {
        phone.SetActive(false);
    }

    private void Update() {

        time.text = System.DateTime.Now.ToString();
    }

    public void CandyGameStart(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
