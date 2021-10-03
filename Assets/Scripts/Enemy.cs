using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] waypoints;

    private float hitDelay = 1f;
    private float lastHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.transform.tag == "Player"){
            Debug.Log("Enemy hit player");
            CharacterController.player.TakeDamage(25);
            lastHit = Time.time;
        }
    }

    void OnCollisionStay2D(Collision2D coll){
        Debug.Log(coll.transform.name);
        if(coll.transform.tag == "Player" && ((Time.time - lastHit) > hitDelay)){
            Debug.Log("Enemy hit player again");
            CharacterController.player.TakeDamage(25);
            lastHit = Time.time;
        }
    }
}
