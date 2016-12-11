using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    private GameObject player;
    private ShipStats stats;
    private Text vel;
    private Text currentLap;
    private Text totalLaps;
    private Text shield;
    private Image shieldBar;
    private Text health;
    private Image healthBar;
    private Text time;

    private float startTime = 0;
    private float stageTime = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player)
        {
            GameObject canvasObject = GameObject.FindGameObjectsWithTag("MainCanvas")[0];
            health = canvasObject.transform.FindChild("HealthBar/Health").GetComponent<Text>();
            healthBar = canvasObject.transform.FindChild("HealthBar/CurrentHealthBar").GetComponent<Image>();
            vel = canvasObject.transform.FindChild("Velocity").GetComponent<Text>();
            currentLap = canvasObject.transform.FindChild("CurrentLap").GetComponent<Text>();
            totalLaps = canvasObject.transform.FindChild("TotalLaps").GetComponent<Text>();
            shield = canvasObject.transform.FindChild("ShieldBar/Center/Shield").GetComponent<Text>();
            shieldBar = canvasObject.transform.FindChild("ShieldBar/CurrentShieldBar").GetComponent<Image>();
            time = canvasObject.transform.FindChild("Time").GetComponent<Text>();
            stats = player.GetComponent<ShipStats>();

            startTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            stageTime = Time.time - startTime;
            int min = ((int)stageTime) / 60;
            time.text = min + ":" + (stageTime % 60).ToString("00.0");
            health.text = stats.health.ToString("0") + " HP" ;

            float ratio = Mathf.Max(0,stats.health / stats.maxHealth);
            healthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);


            currentLap.text = stats.currentLap.ToString();
            totalLaps.text = "3";
            shield.text = stats.shield.ToString() + "%";
            shieldBar.fillAmount = stats.shield;
            int speed = (int)player.GetComponent<Rigidbody>().transform.InverseTransformDirection(player.GetComponent<Rigidbody>().velocity).z;
            speed /= 5;
            vel.text = Mathf.Max(0,speed).ToString();
        }
    }
}
