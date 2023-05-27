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
    private int someoneAttacked = 0;

    void Start()
    {
        RegisterGridNumber();
        RegisterGridXY(MAX_X, MAX_Y, grids.Count);
    }

    public void RegisterGridNumber()
    {
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].gridNumber = i; 
        }
    }

    public void RegisterGridXY(int X, int Y, int gridsTotal)
    {
        int i = 0;
        int j, k;
        while (i < gridsTotal)
        {
            for (j = 0; j < X; j++)
            {
                for (k = 0; k < Y; k++)
                {
                    grids[i].gridX = j;
                    grids[i].gridY = k;
                    i++;
                }
            }
        }
    }

    public void BlockPopRequirement(int btc, int sbctp)
    {
        blockTypeCount = btc;
        sameBlockCountToPop = sbctp;
        for (int i = 0; i < blockTypeCount; i++)
        {
            BlockCountPerType[i] = 0;
        }
    }

    public void GridClickedGiveOrder(int gridNumber)
    {
        if (grids[gridNumber].gridFull == 0)
        {
            FillGrid(gridNumber);
            IsItDangerZone(gridNumber); //validation 1
        }
    }

    public void FillGrid(int gridNumber)
    {
        grids[gridNumber].blockInsideGrid = blockReadyToPut;
        grids[gridNumber].GetComponent<SpriteRenderer>().sprite = grids[gridNumber].blockInsideGrid.GetComponent<SpriteRenderer>().sprite;
        grids[gridNumber].GridFilled();
        blockReadyToPut = null;
    }

    void IsItDangerZone(int gridNumber)
    {
        if (grids[gridNumber].dangerZoneStack == 0)
        {
            //validation 1 passed
            DangerOrSafeZoningFromBlockType(0, gridNumber);
            IsItNotAttackingAnyone(gridNumber); //validation 2
        }
        else
        {
            manager.GameOver();
        }
    }

    public void IsItNotAttackingAnyone(int gridNumber)
    {
        if (someoneAttacked == 0)
        {
            //Validation 2 passed. All's safe.
            //Give score
            manager.ScoreAdding(grids[gridNumber].blockInsideGrid.blockScore);
            //Count if any block type has already three in the board
            BlockCountPerType[grids[gridNumber].blockInsideGrid.blockId] += 1;
            MoreThanTheresHoldToPop(grids[gridNumber].blockInsideGrid.blockId);
            //Ask timer to reset and ready to have another block
            FillDoneProcess();
        }
        else
        {
            manager.GameOver();
        }
    }

    public void ABlockAttacked()
    {
        someoneAttacked = 1;
    }

    void MoreThanTheresHoldToPop(int blockID)
    {
        if (BlockCountPerType[blockID] == sameBlockCountToPop)
        {
            for (int gridNum = 0; gridNum < grids.Count; gridNum++)  //looking in grid list
            {
                if (grids[gridNum].gridFull == 1)
                {
                    if (grids[gridNum].blockInsideGrid.blockId == blockID)
                    {
                        //safezoning from block type cause
                        DangerOrSafeZoningFromBlockType(1, gridNum);
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

    void DangerOrSafeZoningFromBlockType(int dangerOrSafe,int gridNumber)
    {
        //for dangerOrSafe --> danger = 0, safe = 1;

        int tempX = grids[gridNumber].gridX;
        int tempY = grids[gridNumber].gridY;

        if (grids[gridNumber].blockInsideGrid.blockType == 0)  //if the block is Bishop
        {
            //right up x-1 y+1
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 1;
            tempY += 1;
            while (tempX >= 0 && tempY < MAX_Y)
            {
                for (int i=0; i<grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if(dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
                tempX -= 1;
                tempY += 1;
            }
            //right down x+1 y+1
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 1;
            tempY += 1;
            while (tempX < MAX_X && tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
                tempX += 1;
                tempY += 1;
            }
            //left down x+1 y-1
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 1;
            tempY -= 1;
            while (tempX < MAX_X && tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
                tempX += 1;
                tempY -= 1;
            }
            //left up x-1 y-1
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 1;
            tempY -= 1;
            while (tempX >= 0 && tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
                tempX -= 1;
                tempY -= 1;
            }

        }
        if (grids[gridNumber].blockInsideGrid.blockType == 1)  //if the block is Rock
        {
            //right x+0 y+1
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempY += 1;
            while (tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
                tempY += 1;
            }
            //down x+1 y+0
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 1;
            while (tempX < MAX_X)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
                tempX += 1;
            }
            //left x+0 y-1
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempY -= 1;
            while (tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
                tempY -= 1;
            }
            //up x-1 y+0
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 1;
            while (tempX >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
                tempX -= 1;
            }
        }
        if (grids[gridNumber].blockInsideGrid.blockType == 2)  //if the block is Knight
        {
            //up right x-2 y+1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 2;
            tempY += 1;
            if (tempX >= 0 && tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //up right x-1 y+2 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 1;
            tempY += 2;
            if (tempX >= 0 && tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //down right x+1 y+2 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 1;
            tempY += 2;
            if (tempX < MAX_X && tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //down right x+2 y+1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 2;
            tempY += 1;
            if (tempX < MAX_X && tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //down left x+2 y-1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 2;
            tempY -= 1;
            if (tempX < MAX_X && tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //down left x+1 y-2 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 1;
            tempY -= 2;
            if (tempX < MAX_X && tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //up left x-1 y-2 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 1;
            tempY -= 2;
            if (tempX >= 0 && tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //up left x-2 y-1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 2;
            tempY -= 1;
            if (tempX >= 0 && tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
        }
        if (grids[gridNumber].blockInsideGrid.blockType == 3)  //if the block is Dragon
        {
            //right up x-1 y+1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 1;
            tempY += 1;
            if (tempX >= 0 && tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //right down x+1 y+1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 1;
            tempY += 1;
            if (tempX < MAX_X && tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //left down x+1 y-1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 1;
            tempY -= 1;
            if (tempX < MAX_X && tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //left up x-1 y-1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 1;
            tempY -= 1;
            if (tempX >= 0 && tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //right x+0 y+1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempY += 1;
            if (tempY < MAX_Y)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //down x+1 y+0 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX += 1;
            if (tempX < MAX_X)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //left x+0 y-1 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempY -= 1;
            if (tempY >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
            //up x-1 y+0 just once
            tempX = grids[gridNumber].gridX;
            tempY = grids[gridNumber].gridY;
            tempX -= 1;
            if (tempX >= 0)
            {
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].gridX == tempX && grids[i].gridY == tempY)
                    {
                        if (dangerOrSafe == 0)
                        {
                            grids[i].TurnToDangerZone();
                        }
                        else
                        {
                            grids[i].TurnToSafeZone();
                        }
                    }
                }
            }
        }

    }

}
