using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject theBlockManager;
    public List<BlockController> blockTemplateList;
    private BlockController blockNow;
    private int nextBlocknumber = 0;

    public GameManager manager;

    void Start()
    {
        UpdateBlockNow(nextBlocknumber);
    }

    void UpdateBlockNow(int nextBlocknumber)
    {
        if (blockTemplateList.Count == 0)
        {
            return;
        }
        else
        {
            blockNow = blockTemplateList[nextBlocknumber];
            theBlockManager.GetComponent<SpriteRenderer>().sprite = blockNow.GetComponent<SpriteRenderer>().sprite;
            manager.SendToGameManager(blockNow);
        }
    }

    public void NextBlock()
    {
        nextBlocknumber = nextBlocknumber + 1;
        if (nextBlocknumber < blockTemplateList.Count)
        {
            UpdateBlockNow(nextBlocknumber);
        }
        else if (nextBlocknumber == blockTemplateList.Count)
        {
            nextBlocknumber = 0;
            UpdateBlockNow(nextBlocknumber);
        }
    }
}
