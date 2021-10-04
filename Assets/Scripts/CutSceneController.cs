using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public Text menuText;
    public GameObject[] cutScenes;
    public int cutSceneNumber;

    public GameObject cutSceneMenu;
    public GameObject playAgainMenu;

    private CutSceneText currentSceneText;
    private int sceneTextNumber;

    // Start is called before the first frame update
    void Start()
    {
        cutSceneNumber = 0;
        playAgainMenu.SetActive(false);
        cutSceneMenu.SetActive(true);
        LoadCurrentScene();
    }

    private void LoadCurrentScene(){
        currentSceneText = cutScenes[cutSceneNumber].GetComponent<CutSceneText>();
        sceneTextNumber = 0;
        menuText.text = currentSceneText.strings[0];
    }

    public void NextScene(){
        cutSceneNumber += 1;

        if(cutSceneNumber > cutScenes.Length - 1){
            cutSceneMenu.SetActive(false);
            EnablePlayAgainMenu();
        }else{
            cutScenes[cutSceneNumber-1].SetActive(false);
            cutScenes[cutSceneNumber].SetActive(true);
            LoadCurrentScene();
        }
    }

    public void NextText(){
        sceneTextNumber += 1;
        if(sceneTextNumber > currentSceneText.strings.Length - 1){
            NextScene();
        } else{
            menuText.text = currentSceneText.strings[sceneTextNumber];
        }
    }

    public void EnablePlayAgainMenu(){
        playAgainMenu.SetActive(true);
    }
}
