using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
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


    private List<bool> lapTimeShown;
    private List<Transform> laps;


    

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player)
        {
            laps = new List<Transform>();
            lapTimeShown = new List<bool>(3) { false };

            GameObject canvasObject = GameObject.FindGameObjectsWithTag("MainCanvas")[0];
            health = canvasObject.transform.FindChild("HealthBar/Health").GetComponent<Text>();
            healthBar = canvasObject.transform.FindChild("HealthBar/CurrentHealthBar").GetComponent<Image>();
            vel = canvasObject.transform.FindChild("Velocity/Velocity").GetComponent<Text>();
            currentLap = canvasObject.transform.FindChild("Laps/CurrentLap").GetComponent<Text>();
            totalLaps = canvasObject.transform.FindChild("Laps/TotalLaps").GetComponent<Text>();
            shield = canvasObject.transform.FindChild("ShieldBar/Center/Shield").GetComponent<Text>();
            shieldBar = canvasObject.transform.FindChild("ShieldBar/CurrentShieldBar").GetComponent<Image>();
            time = canvasObject.transform.FindChild("Time/Time").GetComponent<Text>();
            print(canvasObject.transform.FindChild("LapsTime/Lap0").Find("Time").GetComponent<Text>().GetType());
            //laps.Add();
            //laps.Add(canvasObject.transform.FindChild("LapsTime/Lap1"));
            //laps.Add(canvasObject.transform.FindChild("LapsTime/Lap2"));

            stats = player.GetComponent<ShipStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            int min = ((int)stats.stageTime) / 60;
            time.text = min + ":" + (stats.stageTime % 60).ToString("00.0");
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

            
            int i = 0;
            bool currentLapFound = false;
            while (!currentLapFound && i < lapTimeShown.Count)
            {
                currentLapFound = !lapTimeShown[i];
                if (!lapTimeShown[i] && stats.lapsTime.Count > i)
                {
                    lapTimeShown[i] = true;
                     Transform lap = ((GameObject)GameObject.FindGameObjectsWithTag("MainCanvas")[0]).transform.FindChild("LapsTime/Lap"+ i);
                    lap.gameObject.SetActive(true);
                    lap.Find("Time").GetComponent<Text>().text = stats.lapsTime[i].ToString("0.00");
                }
                i++;
            }
        }
    }

}
