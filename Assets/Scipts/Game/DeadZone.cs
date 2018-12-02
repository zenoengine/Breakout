using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    GameManager gameManager;
	// Use this for initialization
	void Start ()
    {
        GameObject obj = GameObject.Find("GameManager");
        if (obj)
        {
            gameManager = obj.GetComponent<GameManager>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Ball")
        {
            return;
        }

        if (!gameManager)
        {
            return;
        }

        gameManager.GameOver();
    }
}
