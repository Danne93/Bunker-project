using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;


public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool isGamePaused = false;
    public int score;
    Bunker playerScript;
    public GameObject gameOverScreen;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        playerScript = GameObject.Find("Bunker").GetComponent<Bunker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isGameActive)
        {
            PauseGame();
            //OpenBuyMenu();
        }

        if (playerScript.health <= 0)
        {
            healthText.text = "HP: 0";
            isGameActive = false;
            GameOver();
        }
        else
        {
            healthText.text = "HP: " + playerScript.health;
        }

        scoreText.text = "Score: " + score;
    }

    public void AddScore(int i)
    {
        score = score + i;
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void OpenBuyMenu()
    {

    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
