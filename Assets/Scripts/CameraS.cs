using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraS : MonoBehaviour
{
    Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        transform.position = new Vector3(player.position.x,player.position.y,transform.position.z);
    }
}
