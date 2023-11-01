using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
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

    //Level timer
    private float timer;
    private float minutes;
    private float seconds;

    //Lives
    private float currLives;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); ;
    }

    private void Update()
    {
        currLives = playerController.CurrHealth;
        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = timer / 60;
    }

    private void OnGUI()
    {
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        for (int i = 0; i < lives.Length; i++)
        {
            if (i >= currLives)
                lives[i].enabled = false;
            else
                lives[i].enabled = true;
        }
        currAmmo.sprite = playerController.PlayerShoot.Bullets[playerController.PlayerShoot.CurrentBullet].Sprite;
        pointText.text = (playerController.Score * 100).ToString("0000");
    }
}
