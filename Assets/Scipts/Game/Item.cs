using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum ItemID
    {
        None,
        PowerUP,
        Longer,
        Shorter,
    }

    public ItemID itemId = ItemID.None;

    public delegate void ProcessItemFunc();
    Dictionary<ItemID, ProcessItemFunc> itemProcessMap = new Dictionary<ItemID, ProcessItemFunc>();
    void Start()
    {
        itemProcessMap.Add(ItemID.PowerUP, ProcessPowerUp);
        itemProcessMap.Add(ItemID.Longer, ProcessLonger);
        itemProcessMap.Add(ItemID.Shorter, ProcessShorter);
    }

    void Update()
    {
        if (this.transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Paddle")
        {
            return;
        }

        if (!itemProcessMap.ContainsKey(itemId))
        {
            return;
        }

        itemProcessMap[itemId]();

        Destroy(this.gameObject);
    }

    void ProcessPowerUp()
    {
        GameObject ball = GameObject.Find("Ball");
        if (!ball)
        {
            return;
        }

        ball.SendMessage("OnPowerUpItem");
        SoundManager.Instance.PlaySound("PowerUp", true);
    }

    void ProcessLonger()
    {
        GameObject paddle = GameObject.Find("Paddle");
        if (!paddle)
        {
            return;
        }

        paddle.SendMessage("OnLongerItem");
        SoundManager.Instance.PlaySound("Longer", true);
    }

    void ProcessShorter()
    {
        GameObject paddle = GameObject.Find("Paddle");
        if (!paddle)
        {
            return;
        }

        paddle.SendMessage("OnShorterItem");
        SoundManager.Instance.PlaySound("Shorter", true);
    }
}
