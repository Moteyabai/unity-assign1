using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject hideHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        hideHealthBar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        hideHealthBar?.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
