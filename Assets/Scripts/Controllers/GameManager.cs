using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Attributes
    //Panels
    [Header("Panels")]
    [SerializeField]
    private GameObject endPanel;
    [SerializeField]
    private GameObject nextLevel;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject eventController;
    [SerializeField]
    private GameObject player;

    private int currentScene;

    //Singleton
    public static GameManager gameManager;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        currentScene = 1;

        gameManager = this;

        endPanel.SetActive(false);

        Time.timeScale = 1.0f;

        DontDestroyOnLoad(HUD);
        DontDestroyOnLoad(eventController);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(this);
    }
    #endregion

    #region Management Functions
    /// <summary>
    /// Shows loose panel
    /// </summary>
    public void LoseGame()
    {
        endPanel.SetActive(true);
        nextLevel.SetActive(false);
        endPanel.transform.GetChild(6).GetComponentInChildren<TextMeshProUGUI>().text = "Game Over";

        Time.timeScale = 0;
    }

    /// <summary>
    /// Shows win panel
    /// </summary>
    public void WinGame()
    {

        endPanel.SetActive(true);
        endPanel.transform.GetChild(6).GetComponentInChildren<TextMeshProUGUI>().text = "You Win";

        Time.timeScale = 0;
    }

    /// <summary>
    /// Changes the scene of the game
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene()
    {
        SceneManager.LoadScene(currentScene + 1);
        currentScene++;
        endPanel.SetActive(false);

        Time.timeScale = 1;
    }

    /// <summary>
    /// Changes the scene of the game
    /// </summary>
    /// <param name="sceneName"></param>
    public void MainMenu()
    {
        Destroy(player);
        Destroy(HUD);
        Destroy(eventController);
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
    #endregion
}
