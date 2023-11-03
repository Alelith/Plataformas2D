using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject creditPanel;

    private void Update()
    {
        if (Input.anyKeyDown && creditPanel.activeSelf)
        {
            creditPanel.SetActive(false);
        }
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnCreditButton()
    {
        creditPanel.SetActive(true);
    }
}
