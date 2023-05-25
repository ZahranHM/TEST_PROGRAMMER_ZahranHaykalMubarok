using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameManager manager;
    public Text finalScore;
    
    public void ShowScoreGameOver(int score)
    {
        finalScore.text = score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
    }
    
}
