using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]public Text scoreText;
    private uint score ;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("candy")){
            score++;
            CandySpawner.AccelarateSpawnRate();
        }
    }
    private void Update() {
        scoreText.text = "Score:"+ score.ToString();
    }
    

}
