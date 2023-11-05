using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Panels
    [Header("Panels")]
    [SerializeField]
    private GameObject endPanel;

    //Singleton
    public static GameManager gameManager;

    private void Awake()
    {
        gameManager = this;

        endPanel.SetActive(false);

        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Shows loose panel
    /// </summary>
    public void LoseGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Game Over";

        Time.timeScale = 0;
    }

    /// <summary>
    /// Shows win panel
    /// </summary>
    public void WinGame()
    {

        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "You Win";

        Time.timeScale = 0;
    }

    /// <summary>
    /// Changes the scene of the game
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
