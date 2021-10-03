using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D coll){
        
        if(coll.transform.tag == "Player" && Input.GetKey(KeyCode.E) && CharacterController.player.itemHeld == null){
            CharacterController.player.HoldItem(gameObject);
            Debug.Log(coll.transform.name);
        }
    }
}
