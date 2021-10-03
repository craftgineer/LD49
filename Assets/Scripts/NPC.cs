using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    bool isHappy;
    bool badInRange;
    public GameObject sadFace;
    public GameObject happyFace;
    public GameObject happyMessage;
    // Start is called before the first frame update
    void Start()
    {
        badInRange = true;
        if(CharacterController.player.wordUnlocked){
            IsHappy();
        }else{
            happyMessage.SetActive(false);
            sadFace.SetActive(true);
            happyFace.SetActive(false);
        }   
    }

    // maybe switch to Physics.OverlapSphere(https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html)
    void OnTriggerStay2D(Collider2D coll){
        if(!isHappy){
            if(coll.transform.tag == "Bad"){
                badInRange = true;
            } else if(coll.transform.tag == "Good" && !badInRange){
                IsHappy();
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.transform.tag == "Bad"){
            badInRange = false;
        }
    }

    private void IsHappy(){
        isHappy = true;
        happyMessage.SetActive(true);
        sadFace.SetActive(false);
        happyFace.SetActive(true);
        CharacterController.player.wordUnlocked = true;
        //TODO: prompt player with word attack button
    }

}
