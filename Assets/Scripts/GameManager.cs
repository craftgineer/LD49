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

    private int unlockedCount;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        unlockedCount = 0;
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
    }

    void UnlockBossDoor(){
        //TODO
    }
}
