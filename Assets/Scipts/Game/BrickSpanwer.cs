using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpanwer : MonoBehaviour
{
    public GameObject brick;

    void Start()
    {
        Transform spawnPoint = GameObject.Find("BrickSapwnPoint").transform;
        float blockDist = 2.0f;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector3 spawnLocation = new Vector3(spawnPoint.position.x + i * (brick.transform.localScale.x + blockDist), spawnPoint.position.y - j * 3);
                Instantiate(brick, spawnLocation, Quaternion.identity);
            }
        }
    }
}
