using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    bool bGameOver = false;

    List<GameObject> gameBroadCastList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        GameObject ball = GameObject.Find("Ball");
        if (!ball)
        {
            print("Error: not found ball");
        }
        GameObject itemSpawner = GameObject.Find("ItemSpawner");
        if (!itemSpawner)
        {
            print("Error: not found item spawner");
        }
        GameObject uiManager = GameObject.Find("GameUIManager");
        if (!uiManager)
        {
            print("Error: uiManager not found");
        }

        gameBroadCastList.Add(ball);
        gameBroadCastList.Add(itemSpawner);
        gameBroadCastList.Add(uiManager);
    }

    public void GameOver()
    {
        if (bGameOver)
        {
            return;
        }

        bGameOver = true;

        foreach (var obj in gameBroadCastList)
        {
            obj.SendMessage("OnGameOver");
        }
    }

    void StartGame()
    {
        bGameOver = false;
    }

    bool bShoulCheckGameClear = false;
    public void OnDestroyBrick()
    {
        bShoulCheckGameClear = true;
    }

    private void LateUpdate()
    {
        if (!bShoulCheckGameClear)
        {
            return;
        }

        GameObject[] brickObjs = GameObject.FindGameObjectsWithTag("Brick");
        if (brickObjs.Length == 0)
        {
            // Clear
            foreach (var obj in gameBroadCastList)
            {
                obj.SendMessage("OnGameClear");
            }
        }

        bShoulCheckGameClear = false;
    }
}
