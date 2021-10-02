using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public bool wordUnlocked;
    public bool doubleJumpUnlocked;
    public bool regenUnlocked;
    public bool dashUnlocked;
    public bool hugUnlocked;

    public bool isDead;

    public string currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
