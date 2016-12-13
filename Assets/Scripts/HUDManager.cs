using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class HUDManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rbPlayer;
    private Text vel;
    private Text currentLapText;
    private Text totalLaps;
    private Text shieldText;
    private Image shieldBar;
    private Text healthText;
    private Image healthBar;
    private Text time;


    private int lapTimeToShow;




    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player)
        {
            lapTimeToShow = 0;

            GameObject canvasObject = GameObject.FindGameObjectsWithTag("MainCanvas")[0];
            healthText = canvasObject.transform.FindChild("HealthBar/Health").GetComponent<Text>();
            healthBar = canvasObject.transform.FindChild("HealthBar/CurrentHealthBar").GetComponent<Image>();
            vel = canvasObject.transform.FindChild("Velocity/Velocity").GetComponent<Text>();
            currentLapText = canvasObject.transform.FindChild("Laps/CurrentLap").GetComponent<Text>();
            totalLaps = canvasObject.transform.FindChild("Laps/TotalLaps").GetComponent<Text>();
            shieldText = canvasObject.transform.FindChild("ShieldBar/Center/Shield").GetComponent<Text>();
            shieldBar = canvasObject.transform.FindChild("ShieldBar/CurrentShieldBar").GetComponent<Image>();
            time = canvasObject.transform.FindChild("Time/Time").GetComponent<Text>();

            rbPlayer = player.GetComponent<Rigidbody>();

            totalLaps.text = "3";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {

            int speed = (int)rbPlayer.transform.InverseTransformDirection(rbPlayer.velocity).z;
            speed /= 5;
            vel.text = Mathf.Max(0, speed).ToString();

        }
    }

    public void updateTime(float stageTime)
    {
        time.text = minSec(stageTime);
    }

    public void updatehealth(float health, float maxHealth)
    {
        healthText.text = health.ToString("0") + " HP";
        float ratio = Mathf.Max(0, health / maxHealth);
        healthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    public void updateShield(float shield)
    {
        shieldText.text = shield.ToString() + "%";
        shieldBar.fillAmount = shield;
    }

    public void updateLap(int currentLap)
    {
        currentLapText.text = currentLap.ToString();
    }

    public void updateLapTime(int lap, float lapTime)
    {
        Transform lapTransform = ((GameObject)GameObject.FindGameObjectsWithTag("MainCanvas")[0]).transform.FindChild("LapsTime/Lap" + lap);
        lapTransform.gameObject.SetActive(true);
        lapTransform.Find("Time").GetComponent<Text>().text = minSec(lapTime);
    }

    private string minSec(float time)
    {
        int min = ((int)time) / 60;
        return min + ":" + (time % 60).ToString("00.0");
    }

}
