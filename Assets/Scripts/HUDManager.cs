using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class HUDManager : MonoBehaviour
{
    private Text vel;
    private Text currentLapText;
    private Text shieldText;
    private Image shieldBar;
    private Text healthText;
    private Image healthBar;
    private Text time;

    public int totalLaps;

    private int lapTimeToShow;

    // Use this for initialization
    void Start()
    {
        lapTimeToShow = 0;

        GameObject canvasObject = GameObject.FindGameObjectsWithTag("MainCanvas")[0];
        healthText = canvasObject.transform.FindChild("HealthBar/Health").GetComponent<Text>();
        healthBar = canvasObject.transform.FindChild("HealthBar/CurrentHealthBar").GetComponent<Image>();
        vel = canvasObject.transform.FindChild("Velocity/Velocity").GetComponent<Text>();
        currentLapText = canvasObject.transform.FindChild("Laps/CurrentLap").GetComponent<Text>();
        shieldText = canvasObject.transform.FindChild("ShieldBar/Center/Shield").GetComponent<Text>();
        shieldBar = canvasObject.transform.FindChild("ShieldBar/CurrentShieldBar").GetComponent<Image>();
        time = canvasObject.transform.FindChild("Time/Time").GetComponent<Text>();

        canvasObject.transform.FindChild("Laps/TotalLaps").GetComponent<Text>().text = totalLaps.ToString();
    }

    public void setCamera(Camera camera)
    {
        gameObject.GetComponent<Canvas>().worldCamera = camera;
    }

    public void setTotalLaps(int laps)
    {
        totalLaps = laps;
    }

    public void updateSpeed(float speed)
    {
        vel.text = Mathf.Max(0, speed).ToString();
    }

    public void updateTime(float stageTime)
    {
        time.text = minSec(stageTime);
    }

    public void updateHealth(float health, float maxHealth)
    {
        healthText.text = health.ToString("0") + " HP";
        float ratio = Mathf.Max(0, health / maxHealth);
        healthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    public void updateShield(float shield)
    {
        shieldText.text = shield.ToString("0") + "%";
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
