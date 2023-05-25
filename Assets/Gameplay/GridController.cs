using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public BoardManager manager;
    public int gridNumber;
    public BlockController blockInsideGrid;
    public int gridFull = 0;
    public int dangerZoneGrid = 0;
    public Sprite emptySprite;
    public Sprite dangerZoneSprite;

    public void GridFilled()
    {
        gridFull = 1;
    }
    public void GridEmptied()
    {
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

    void DangerZoned()
    {
        dangerZoneGrid = 1;
        this.GetComponent<SpriteRenderer>().sprite = dangerZoneSprite;
    }

}
