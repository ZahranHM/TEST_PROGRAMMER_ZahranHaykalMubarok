using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject theBlockManager;
    public List<GameObject> blockTemplateList;
    private GameObject blockNow;

    public GameManager manager;

    void Start()
    {
        UpdateBlockNow();
    }

    void UpdateBlockNow()
    {
        if (blockTemplateList.Count == 0)
        {
            return;
        }
        else
        {
            blockNow = blockTemplateList[0];
            theBlockManager.GetComponent<SpriteRenderer>().sprite = blockNow.GetComponent<SpriteRenderer>().sprite;
            manager.SendToGameManager(blockNow);
        }
    }

}
