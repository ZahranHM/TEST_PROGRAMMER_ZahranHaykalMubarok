using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject theBlockManager;
    public List<BlockController> blockTemplateList;
    private BlockController blockNow;

    public GameManager manager;

    void Start()
    {
        RegisterBlockId();
        manager.BlockTypeCount(blockTemplateList.Count);
        UpdateBlockNow();
    }

    void RegisterBlockId()
    {
        for (int i = 0; i < blockTemplateList.Count; i++)
        {
            blockTemplateList[i].blockId = i;  //Block Id register
        }
    }

    void UpdateBlockNow()
    {
        if (blockTemplateList.Count == 0)
        {
            return;
        }
        else
        {
            blockNow = blockTemplateList[Random.Range(0, blockTemplateList.Count)];
            theBlockManager.GetComponent<SpriteRenderer>().sprite = blockNow.GetComponent<SpriteRenderer>().sprite;
            manager.SendToGameManager(blockNow);
        }
    }

    public void NextBlock()
    {
        UpdateBlockNow();
    }

}
