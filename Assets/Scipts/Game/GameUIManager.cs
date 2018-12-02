using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameClearUI;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        SoundManager.Instance.PlayMusic("GameBGM", true);
    }
    public void OnReturnToMainMenuButtonClicked()
    {
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        SoundManager.Instance.StopAllCoroutines();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnGameOver()
    {
        gameOverUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySound("GameOver", true);
    }

    public void OnGameClear()
    {
        gameClearUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySound("GameClear", true);
    }
}

