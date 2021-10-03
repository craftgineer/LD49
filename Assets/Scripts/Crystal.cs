using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public int crystalNumber;

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.transform.tag == "Hug" || coll.transform.tag == "Word"){
            //TODO: Death animation
            GameManager.gm.CrystalBroken(crystalNumber);
            Destroy(gameObject);
        }
    }
}
