using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject hideHealthBar;
    private PlayerMoment playerMoment;
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        hideHealthBar.SetActive(true);
        playerMoment = FindObjectOfType<PlayerMoment>();
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

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        playerMoment.isDied = false;
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
