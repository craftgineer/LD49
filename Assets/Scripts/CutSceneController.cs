using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public static CutSceneController csc;

    public Text menuText;
    public GameObject[] cutScenes;
    public int cutSceneNumber;

    public GameObject cutSceneMenu;
    public GameObject playAgainMenu;

    private CutSceneText currentSceneText;
    private int sceneTextNumber;

    public Animator BossAnim;
    public bool eventProcessed;
    
    public GameObject[] npcHeart;
    public GameObject[] npcSmile;
    public GameObject[] npcSad;

    // Start is called before the first frame update
    void Start()
    {
        csc = this;
        cutSceneNumber = 0;
        playAgainMenu.SetActive(false);
        cutSceneMenu.SetActive(true);
        foreach(GameObject scene in cutScenes){
            scene.SetActive(false);
        }
        SetNPCStates();
        LoadCurrentScene();
    }

    private void LoadCurrentScene(){
        cutScenes[cutSceneNumber].SetActive(true);
        eventProcessed = false;
        currentSceneText = cutScenes[cutSceneNumber].GetComponent<CutSceneText>();
        sceneTextNumber = 0;
        menuText.text = currentSceneText.strings[0];
    }

    public void NextScene(){
        cutSceneNumber += 1;

        if(cutSceneNumber > cutScenes.Length - 1){
            cutScenes[cutSceneNumber-1].SetActive(false);
            cutSceneMenu.SetActive(false);
            EnablePlayAgainMenu();
        }else{
            cutScenes[cutSceneNumber-1].SetActive(false);
            LoadCurrentScene();
        }
    }

    public void NextText(){
        sceneTextNumber += 1;
        if(sceneTextNumber > currentSceneText.strings.Length - 1){
            if(currentSceneText.hasEvent && !eventProcessed){
                ProcessCutSceneEvent(currentSceneText.eventID);
            }else {
                NextScene();
            }            
        } else{
            menuText.text = currentSceneText.strings[sceneTextNumber];
        }
    }

    public void EnablePlayAgainMenu(){
        playAgainMenu.SetActive(true);
    }

    private void ProcessCutSceneEvent(int eventID){
        switch(eventID){
            case 1:
                BossAnim.SetTrigger("bossDead");
                break;
        }
    }

    public void EventProcessed(){
        NextScene();
    }

    private void SetNPCStates(){
        for(int i = 0; i < 5; i++){
            if(PlayerPrefs.GetInt("npc" + i + "_helped", 0) == 1){
                npcHeart[i].SetActive(true);
                npcSmile[i].SetActive(true);
                npcSad[i].SetActive(false);
            } else {
                npcHeart[i].SetActive(false);
                npcSmile[i].SetActive(false);
                npcSad[i].SetActive(true);
            }
        }
    }
}
