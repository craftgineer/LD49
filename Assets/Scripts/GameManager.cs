using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        unlockedCount = 0;
        musicPhase2Triggered = false;
        musicPhase3Triggered = false;
        BossDoorOpen.SetActive(false);
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
