using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour {

    private void Start()
    {
        SoundManager.Instance.PlayMusic("MainMenuBGM", true);
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Game");
        Cursor.visible = false;
    }
}
