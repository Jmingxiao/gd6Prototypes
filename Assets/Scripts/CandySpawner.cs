using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    
    static float updaterate = 3.0f;
    float timer =0.0f;
    ObjectPool objectPool ;
    private void Start() {
        objectPool = ObjectPool.Instance;    
    }
    private void Update() 
    {   timer += Time.deltaTime;
        SpawnCandy();
    }

    void SpawnCandy()
    {   
        if(timer>updaterate){
            
            timer = 0.0f;
            var position = transform.position + Vector3.left * Random.Range(-Config.SCRWidth,Config.SCRWidth);
            objectPool.SpawnFromPool("candy",position,Quaternion.identity);
        }
    }
   public static void AccelarateSpawnRate(){
        if(updaterate<=Config.MinCandySpawnRate){
            return;
        }
        updaterate-= Random.Range(0.1f,0.15f);
        Debug.Log(updaterate);
    }

}
