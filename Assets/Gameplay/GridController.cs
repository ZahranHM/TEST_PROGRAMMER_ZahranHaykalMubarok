using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public BoardManager manager;
    public int gridNumber;
    public BlockController blockInsideGrid;
    public int gridFull = 0;
    public int dangerZoneStack = 0;
    public int gridX;
    public int gridY;
    public Sprite emptySprite;
    public Sprite dangerZoneSprite;

    public void GridFilled()
    {
        gridFull = 1;
    }
    public void GridEmptied()
    {
        blockInsideGrid = null;
        this.GetComponent<SpriteRenderer>().sprite = emptySprite;
        gridFull = 0;
    }

    public void OnMouseDown()
    {
        manager.GridClickedGiveOrder(gridNumber);
    }

    public void TurnToDangerZone()
    {
        DangerZoned();
    }

    public void TurnToSafeZone()
    {
        SafeZoned();
    }

    void DangerZoned()
    {
        dangerZoneStack += 1;
        this.GetComponent<SpriteRenderer>().sprite = dangerZoneSprite;
        if (gridFull == 1)
        {
            manager.ABlockAttacked();
        }
    }

    void SafeZoned()
    {
        if (gridFull == 0)
        {
            dangerZoneStack -= 1;
            if (dangerZoneStack == 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = emptySprite;
            } 
        }
    }

}
