using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    public GameObject EasyLevelProps;
    public GameObject NormalLevelProps;
    public GameObject HardLevelProps;

    private void Awake() 
    {
        if(DifficultyManager.difficulty == 1)
        {
            EasyLevelProps.SetActive(true);
            NormalLevelProps.SetActive(false);
            HardLevelProps.SetActive(false);
        }
        if(DifficultyManager.difficulty == 2)
        {
            EasyLevelProps.SetActive(true);
            NormalLevelProps.SetActive(true);
            HardLevelProps.SetActive(false);
        }
        
        if(DifficultyManager.difficulty == 3)
        {
            EasyLevelProps.SetActive(true);
            NormalLevelProps.SetActive(true);
            HardLevelProps.SetActive(true);
        }
    }
    
}
