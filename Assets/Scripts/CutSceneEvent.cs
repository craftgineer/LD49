using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEvent : MonoBehaviour
{
    public void CutSceneOver(){
        CutSceneController.csc.EventProcessed();
    } 
    
}
