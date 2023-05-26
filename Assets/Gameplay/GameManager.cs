using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BlockManager blockManager;
    public BoardManager boardManager;
    public ScoreController scoreController;
    public TimeManager timeManager;
    public GameOverController gameOverController;
    public GameObject gameOverCanvas;
    public BlockController tempBlock;
    public int score;

    public void SendToGameManager(BlockController block)
    {
        tempBlock = block;
        boardManager.blockReadyToPut = tempBlock;
    }

    public void ScoreAdding(int newAdditionalScore)
    {
        score = score + newAdditionalScore;
        Debug.Log("Score = " + score);
    }

    public void SignalToBlockManagerImDone()
    {
        tempBlock = null;
        blockManager.NextBlock();
    }

    public void ResetTheTimer()
    {
        timeManager.ResetTimerPlease();
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        gameOverController.ShowScoreGameOver(score);
        boardManager.GridClosing();
        timeManager.GameAlreadyOver();
    }

    void Update()
    {
        
    }
}
