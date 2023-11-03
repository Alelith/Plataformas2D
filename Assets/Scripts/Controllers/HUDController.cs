using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    #region Attributes
    //HUD
    [Header("HUD")]
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI pointText;
    [SerializeField]
    private Image[] lives; 
    [SerializeField]
    private Image currAmmo; 
    [SerializeField]
    private RectTransform energy; 

    //Level timer
    private float timer;
    private float minutes;
    private float seconds;

    //Lives
    private float currLives;
    private int currEnergy;
    private float energyHeight;
    private PlayerController playerController;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        //Obtains the PlayerController of the scene 
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); ;
        energyHeight = energy.rect.height;
    }

    private void Update()
    {
        //Gets and update the HUD Info
        currEnergy = playerController.PlayerShoot.CurrEnergy;
        currLives = playerController.CurrHealth;
        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = timer / 60;
    }

    private void OnGUI()
    {
        //Refresh the info on GUI
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        for (int i = 0; i < lives.Length; i++)
        {
            if (i >= currLives)
                lives[i].enabled = false;
            else
                lives[i].enabled = true;
        }
        energy.sizeDelta = new Vector2(energy.rect.width ,currEnergy * energyHeight / playerController.PlayerShoot.MaxEnergy);
        currAmmo.sprite = playerController.PlayerShoot.Bullets[playerController.PlayerShoot.CurrentBullet].Sprite;
        pointText.text = (playerController.Score * 100).ToString("0000");
    }
    #endregion
}
