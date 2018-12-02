using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    int blockCount = 1;
    public Color[] colors;
    Animation anim;
    GameObject gameManager;
    void Start()
    {
        anim = GetComponent<Animation>();
        blockCount = Random.Range(1, 4);
        UpdateMaterialByBlockCount();
        gameManager = GameObject.Find("GameManager");
    }

    void UpdateMaterialByBlockCount()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (!renderer)
        {
            return;
        }

        Material mat = GetComponent<Renderer>().material;
        if (!mat)
        {
            return;
        }

        if(blockCount == 0)
        {
            return;
        }

        int index = blockCount - 1;
        if (colors.Length < index)
        {
            index = colors.Length - 1;
        }

        mat.color = colors[index];
    }

    void PlayAnimation()
    {
        if (!anim)
        {
            return;
        }

        anim.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            PlayAnimation();

            blockCount--;
            BallMovement movement = collision.gameObject.GetComponent<BallMovement>();
            if (movement)
            {
                if (movement.isPowerUpMode())
                {
                    blockCount = 0;
                }
            }
            UpdateMaterialByBlockCount();
            if (blockCount <= 0)
            {
                Destroy(this.gameObject);
            }

            if (gameManager)
            {
                gameManager.SendMessage("OnDestroyBrick");
            }
        }
    }
}