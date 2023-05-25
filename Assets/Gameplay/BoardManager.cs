using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private int MAX_X = 6;
    private int MAX_Y = 9;

    public GameManager manager;
    public List<GridController> grids;  //Insert grids that want to participate from Unity
    private List<List<GridController>> gridsPlacement;
    public BlockController blockReadyToPut;

    void Start()
    {
        RegisterGridNumber();
        DangerZoningTest();
    }

    public void RegisterGridNumber()
    {
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].gridNumber = i+1;  //Grid register number start from 1
        }
    }

    //under maintenance
    public void RegisterGridPlacement(int i)
    {
        for (int x = 0; x < MAX_X; x++)
        {
            for (int y = 0; y < MAX_Y; y++)
            {
                gridsPlacement[x][y] = grids[i];
            }
        }
        
    }

    public void GridClickedGiveOrder(int gridNumber)
    {
        gridNumber = gridNumber - 1;   //Change Grid register number into Grid sequence number

        if (grids[gridNumber].gridFull == 0)
        {
            FillGrid(gridNumber);
            GridFillValidation(gridNumber);
            FillDoneProcess();
        }  
    }

    public void FillGrid(int gridNumber)
    {
        grids[gridNumber].blockInsideGrid = blockReadyToPut;
        grids[gridNumber].GetComponent<SpriteRenderer>().sprite = blockReadyToPut.GetComponent<SpriteRenderer>().sprite;
        grids[gridNumber].GridFilled();
    }

    void GridFillValidation(int gridNumber)
    {
        int scoreSendToGameManager;
        if (grids[gridNumber].dangerZoneGrid == 0)
        {
            scoreSendToGameManager = grids[gridNumber].blockInsideGrid.blockScore;
            manager.ScoreAdding(scoreSendToGameManager);
        }
        else 
        {
            Debug.Log("ini harusnya bakal game over.");
            manager.GameOver();
        }
    }

    public void FillDoneProcess()
    {
        blockReadyToPut = null;
        manager.SignalToBlockManagerImDone();
    }

    public void DangerZoningTest()
    {
        int i = 1;
        grids[27 - i].TurnToDangerZone();
    }

    public void GridClosing()
    {
        GridClose();
    }

    void GridClose()
    {
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].gridFull = 1;
        }
    }

    void Update()
    {
        
    }
}
