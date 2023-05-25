using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BlockManager blockManager;
    public BoardManager boardManager;
    public ScoreController scoreController;
    public BlockController tempBlock;
    public int score;

    void Start()
    {
        
    }

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

    void Update()
    {
        
    }
}
