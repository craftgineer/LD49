using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{
    void GameDone(){
        SceneManager.LoadScene("Boss");
    }

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.transform.tag == "Player"){
            GameDone();
        }
    }
}
