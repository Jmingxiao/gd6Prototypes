using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField ]Transform player;
    [SerializeField ]Transform gun;
    ObjectPool objectPool ;
    [HideInInspector]public bool inrange;
    

    // Start is called before the first frame update
    private void Start() {
        objectPool = ObjectPool.Instance;    
    }

    // Update is called once per frame
    void Update()
    {   
       
        if(inrange){
            var angle = Vector2.Angle(gun.position,player.position);
            var bullet =objectPool.SpawnFromPool("Bullet",gun.position,Quaternion.identity).GetComponent<Bullet>();
            bullet.dir = (player.position- transform.position).normalized;
        }
        inrange =false;
         
    }
}
