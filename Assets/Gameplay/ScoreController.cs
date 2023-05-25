using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public GameManager manager;
    public Text UIScore;

    void Update()
    {
        UIScore.text = manager.score.ToString();
    }    

}
