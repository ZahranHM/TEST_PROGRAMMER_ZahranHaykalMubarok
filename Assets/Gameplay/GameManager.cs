using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BlockManager blockManager;
    public BoardManager boardManager;
    public GameObject tempBlock;
    public int score;

    void Start()
    {
        
    }

    public void SendToGameManager(GameObject block)
    {
        tempBlock = block;
        boardManager.currentBlock = tempBlock;
        boardManager.FillGrid();
    }

    void Update()
    {
        
    }
}
