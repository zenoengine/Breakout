using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject powerUpItem;
    public GameObject longerItem;
    public GameObject shorterItem;

    const float defaultSpawnTime = 10;
    float spawnTime = defaultSpawnTime;

    bool bCanSpawn = true;

    List<GameObject> itemList = new List<GameObject>();
    private void Start()
    {
        itemList.Add(powerUpItem);
        itemList.Add(longerItem);
        itemList.Add(shorterItem);
        bCanSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bCanSpawn)
        {
            return;
        }

        spawnTime -= Time.deltaTime;

        if (spawnTime > 0)
        {
            return;
        }

        int index = Random.Range(0, 3);
        if (index < itemList.Count)
        {
            GameObject item = itemList[index];
            float randomPosX = Random.Range(-16, 16);
            Instantiate(item, new Vector3(randomPosX, 20), item.transform.rotation, this.transform);
            spawnTime = defaultSpawnTime;
        }
    }

    public void OnGameOver()
    {
        this.gameObject.SetActive(false);
    }

    public void OnGameClear()
    {
        this.gameObject.SetActive(false);
    }
}
