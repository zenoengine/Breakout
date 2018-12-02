using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    enum BallState
    {
        None,
        PowerUp
    }

    BallState state = BallState.None;

    Vector3 direction = new Vector3();
    public float defaultSpeed = 2000;
    float Speed = 0;
    Rigidbody rb;

    void Start()
    {
        Vector3[] randomDirectionArray = new[] { new Vector3(1f, 1f, 0f), new Vector3(-1f, 1f, 0f), };
        int randomIndex = Random.Range(0, 1);
        direction = randomDirectionArray[randomIndex];
        direction.Normalize();
        rb = GetComponent<Rigidbody>();
        Speed = defaultSpeed;
        rb.AddForce(direction * Speed);
        UpdateMaterialByState();
    }

    private void UpdateForceToDirection()
    {
        direction.Normalize();
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Brick")
        {
            string soundName = "Brick";
            switch (state)
            {
                case BallState.PowerUp:
                    {
                        UpdateForceToDirection();
                        soundName = "BrickBoom";
                        SoundManager.Instance.PlaySound(soundName, true);
                        
                    }
                    return;
                case BallState.None:
                    {
                        SoundManager.Instance.PlaySound(soundName, true);
                    }
                    break;
            }
        }

        if (collision.gameObject.tag == "Wall" && state == BallState.PowerUp)
        {
            Animation cameraAnimation = Camera.main.GetComponent<Animation>();
            if (cameraAnimation)
            {
                cameraAnimation.Play();
                SoundManager.Instance.PlaySound("HitWall", true);
                
            }
        }

        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 reverseDirectionVec = (transform.position - contact.point).normalized;
            float projectionSize = Vector3.Dot(contact.normal, reverseDirectionVec);
            Vector3 reflectionVec = contact.normal * projectionSize * 2 + direction;
            direction = reflectionVec;

            UpdateForceToDirection();
            break;
        }
    }

    public void OnGameOver()
    {
        rb.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    public void OnGameClear()
    {
        this.gameObject.SetActive(false);
    }

    int activatedPowerUp = 0;

    private void UpdateMaterialByState()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (!renderer)
        {
            return;
        }

        switch (state)
        {
            case BallState.None:
                {
                    renderer.material.color = new Color(1, 1, 1);
                    transform.localScale = new Vector3(1, 1, 1);
                }
                break;
            case BallState.PowerUp:
                {
                    renderer.material.color = new Color(1, 1, 0);
                    transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
                }
                break;
        }
    }

    public void OnPowerUpItem()
    {
        state = BallState.PowerUp;
        activatedPowerUp++;
        Speed = 1500;
        UpdateMaterialByState();
        StartCoroutine(EndPowerupEffect());
    }

    public bool isPowerUpMode()
    {
        return state == BallState.PowerUp;
    }

    IEnumerator EndPowerupEffect()
    {
        yield return new WaitForSeconds(5.0f);
        activatedPowerUp--;
        if (activatedPowerUp == 0)
        {
            state = BallState.None;
            Speed = defaultSpeed;
            UpdateMaterialByState();
        }
    }
}
