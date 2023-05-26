using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private int MAX_X = 6;
    private int MAX_Y = 9;

    public GameManager manager;
    public List<GridController> grids;  //Insert grids that want to participate from Unity
    private List<List<GridController>> gridsPlacement;  //UNDER CONSTRUCTION
    public BlockController blockReadyToPut;
    private int blockTypeCount;
    private int sameBlockCountToPop;
    private int[] BlockCountPerType = new int[4]; //HARDCODED, 4 types of block (bishop,rock,knight,dragon)
    
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

    //UNDER CONSTRUCTION
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

    public void BlockPopRequirement(int btc, int sbctp)
    {
        blockTypeCount = btc;
        sameBlockCountToPop = sbctp;
        for(int i=0; i<blockTypeCount; i++)
        {
            BlockCountPerType[i] = 0;
        }
    }

    public void GridClickedGiveOrder(int gridNumber)
    {
        gridNumber = gridNumber - 1;   //Change Grid register number into Grid sequence number

        if (grids[gridNumber].gridFull == 0)
        {
            FillGrid(gridNumber);
            GridFillValidation(gridNumber);
        }  
    }

    public void FillGrid(int gridNumber)
    {
        grids[gridNumber].blockInsideGrid = blockReadyToPut;
        grids[gridNumber].GetComponent<SpriteRenderer>().sprite = grids[gridNumber].blockInsideGrid.GetComponent<SpriteRenderer>().sprite;
        grids[gridNumber].GridFilled();
    }

    void GridFillValidation(int gridNumber)
    {
        if (grids[gridNumber].dangerZoneGrid == 0)
        {
            int scoretosend = grids[gridNumber].blockInsideGrid.blockScore;
            manager.ScoreAdding(scoretosend);
            BlockCountPerType[grids[gridNumber].blockInsideGrid.blockId] += 1;
            blockReadyToPut = null;
            MoreThanTheresHoldToPop(grids[gridNumber].blockInsideGrid.blockId); //INI PIKIRIN
            FillDoneProcess();
        }
        else 
        {
            manager.GameOver();
        }
    }

    void MoreThanTheresHoldToPop(int blockID)
    {
        if(BlockCountPerType[blockID] == sameBlockCountToPop)
        {
            for (int gridNum = 0; gridNum < grids.Count; gridNum++)  //looking in grid list
            {
                if (grids[gridNum].gridFull == 1)
                {
                    if (grids[gridNum].blockInsideGrid.blockId == blockID)
                    {
                        grids[gridNum].GridEmptied();
                    }
                }
            }
            BlockCountPerType[blockID] = 0;
        }
    }

    void FillDoneProcess()
    {
        manager.ResetTheTimer();
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
