using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public Lock LockOne;
    public Lock LockTwo;
    public Lock LockThree;
    public Lock LockFour;
    public Lock LockFive;

    public Sprite DoorSprite;
    public GameObject BossDoorOpen;
    public SpriteRenderer doorSR;

    private int unlockedCount;
    private bool musicPhase2Triggered;
    private bool musicPhase3Triggered;

    public GameObject DeadMenu;
    public GameObject InfoMenu;
    public GameObject StartMenu;
    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        unlockedCount = 0;
        musicPhase2Triggered = false;
        musicPhase3Triggered = false;
        DeadMenu.SetActive(false);
        InfoMenu.SetActive(false);
        BossDoorOpen.SetActive(false);
        StartMenu.SetActive(true);

        PlayerPrefs.SetInt("npc1_helped", 0);
        PlayerPrefs.SetInt("npc2_helped", 0);
        PlayerPrefs.SetInt("npc3_helped", 0);
        PlayerPrefs.SetInt("npc4_helped", 0);
        PlayerPrefs.SetInt("npc5_helped", 0);
    }

    public void ToggleDeadMenu(){
        if(DeadMenu.activeSelf){
            DeadMenu.SetActive(false);
            CharacterController.player.Respawn();
        } else{
            DeadMenu.SetActive(true);
        }
        
    }

    public void ToggleInfoMenu(string info = ""){
        if(InfoMenu.activeSelf){
            InfoMenu.SetActive(false);
        } else{
            infoText.text = info;
            InfoMenu.SetActive(true);
        }
    }

    public void ToggleStartMenu(){
        if(StartMenu.activeSelf){
            StartMenu.SetActive(false);
        } else{
            StartMenu.SetActive(true);
        }
    }

    public void CrystalBroken(int num){
        switch(num){
            case 1:
                LockOne.Unlock();
                break;
            case 2:
                LockTwo.Unlock();
                break;
            case 3:
                LockThree.Unlock();
                break;
            case 4:
                LockFour.Unlock();
                break;
            case 5:
                LockFive.Unlock();
                break;
        }
        unlockedCount += 1;
        CheckLocks();
    }

    public void CheckLocks(){
        if(unlockedCount >= 5){
            UnlockBossDoor();
        }
        if(!musicPhase2Triggered && unlockedCount == 1){
            AudioPlayer.Instance.PlayMusicByName("Wandering/2", null);
            musicPhase2Triggered = true;
        }
        if(!musicPhase3Triggered && unlockedCount == 3){
            AudioPlayer.Instance.PlayMusicByName("Wandering/3", null);
            musicPhase3Triggered = true;
        }

    }

    void UnlockBossDoor(){
        BossDoorOpen.SetActive(true);
        doorSR.sprite = DoorSprite;
    }
}
