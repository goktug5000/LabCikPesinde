using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class MainMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;

    [SerializeField] private GameObject menu;

    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }



        }
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void RestartSection()
    {
        Debug.Log("Restarting");
        Resume();
        StartCoroutine(loadThis(SceneManager.GetActiveScene().name));
    }

    static public bool isFullyLoaded;
    IEnumerator loadThis(string sceneName)
    {
        isFullyLoaded = false;
        SceneManager.LoadScene(sceneName);
        isFullyLoaded = true;

        yield return null;

    }

    public void ExitGame()
    {
        Resume();
        Debug.Log("Exiting the game");
        Application.Quit();
    }
}
