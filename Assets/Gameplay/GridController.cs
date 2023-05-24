using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public BoardManager manager;
    public int gridNumber;
    public GameObject blockInside;
    public int gridFull = 0;
    public int dangerZoneGrid = 0;

    void Start()
    {
        
    }

    public void GridFilled()
    {
        gridFull = 1;
    }
    public void GridEmpty()
    {
        gridFull = 0;
    }

    void Update()
    {
        
    }
}
