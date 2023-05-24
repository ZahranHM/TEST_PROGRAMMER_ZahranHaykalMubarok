using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public GameManager manager;
    public List<GridController> grids;
    public GameObject currentBlock;

    void Start()
    {
        RegisterGrid();
    }

    public void RegisterGrid()
    {
        grids[0].gridNumber = 0;
    }

    public void FillGrid()
    {
        if (grids[0].gridFull == 0)
        {
            grids[0].blockInside = currentBlock;
            grids[0].GetComponent<SpriteRenderer>().sprite = currentBlock.GetComponent<SpriteRenderer>().sprite;
            grids[0].GridFilled();
        }
    }

    void Update()
    {
        
    }
}
