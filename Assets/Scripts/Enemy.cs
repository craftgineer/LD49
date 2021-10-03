using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public bool weakToHug;
    public bool weakToWord;

    private float hitDelay = 1f;
    private float lastHit;

    void OnCollisionEnter2D(Collision2D coll){
        Debug.Log(coll.transform.tag);
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

    void OnTriggerEnter2D(Collider2D coll){
        if (weakToHug && coll.transform.tag == "Hug"){
            //TODO: Death animation
            Destroy(gameObject);
        } else if(weakToWord && coll.transform.tag == "Word"){
            //TODO: Death animation
            Destroy(gameObject);
        }
    }
}
