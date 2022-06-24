using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTitle : MonoBehaviour
{
    private Text gameTitle;

    private void Awake() 
    {
        gameTitle = GetComponent<Text>();
        gameTitle.text = Application.productName;
    }
}
