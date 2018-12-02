using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform leftLimitPoint;
    Transform rightLimitPoint;
    GameObject leftPoint, rightPoint;
    Vector3 ScaleChangingAmount = new Vector3(1.5f, 0, 0);

    const int MAX_SCALE_X = 14;
    const int MIN_SCALE_X = 4;

    float ScaleX = 8;
    float moveSpeed = 2;

    void Start()
    {
        transform.localScale = new Vector3(ScaleX, 1, 1);

        leftPoint = new GameObject("LeftPoint");
        rightPoint = new GameObject("RightPoint");
        leftPoint.transform.parent = this.transform;
        rightPoint.transform.parent = this.transform;
        leftPoint.transform.localPosition = new Vector3(-0.5f, 0);
        rightPoint.transform.localPosition = new Vector3(+0.5f, 0);

        leftLimitPoint = GameObject.Find("LeftLimitPoint").transform;
        rightLimitPoint = GameObject.Find("RightLimitPoint").transform;
    }

    void Update()
    {
        Vector3 nextPos = this.transform.position + new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0); ;
        float leftdDistX = this.transform.position.x - leftPoint.transform.position.x;
        float rightDistX = rightPoint.transform.position.x - this.transform.position.x;
        if (nextPos.x - leftdDistX <= leftLimitPoint.position.x)
        {
            nextPos.x = leftLimitPoint.position.x + leftdDistX;
        }
        if (nextPos.x + rightDistX >= rightLimitPoint.position.x)
        {
            nextPos.x = rightLimitPoint.position.x - rightDistX;
        }
        this.transform.position = nextPos;
    }

    public void OnLongerItem()
    {
        if (transform.localScale.x < MAX_SCALE_X)
        {
            transform.localScale += ScaleChangingAmount;
        }
    }

    public void OnShorterItem()
    {
        if (transform.localScale.x >= MIN_SCALE_X)
        {
            transform.localScale -= ScaleChangingAmount;
        }
    }
}
