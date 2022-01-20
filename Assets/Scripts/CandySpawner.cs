using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    [SerializeField]
    const float width =5.0f;
    [SerializeField]
    const float updaterate = 0.6f;
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
            var position = transform.position + Vector3.left * Random.Range(-width,width);
            objectPool.SpawnFromPool("candy",position,Quaternion.identity);
        }
    }

}
