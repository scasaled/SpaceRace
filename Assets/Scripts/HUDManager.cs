using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    public GameObject player;

    private ShipStats stats;
    private Text vel;
    private Text laps;
    private Text shield;
    private Text health;
    private Text time;

    private float startTime = 0;
    private float stageTime = 0;

    // Use this for initialization
    void Start()
    {
        GameObject canvasObject = GameObject.FindGameObjectsWithTag("MainCanvas")[0];
        health = canvasObject.transform.FindChild("Health").GetComponent<Text>();
        vel = canvasObject.transform.FindChild("Velocity").GetComponent<Text>();
        laps = canvasObject.transform.FindChild("Laps").GetComponent<Text>();
        shield = canvasObject.transform.FindChild("Shield").GetComponent<Text>();
        time = canvasObject.transform.FindChild("Time").GetComponent<Text>();
        stats = player.GetComponent<ShipStats>();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        stageTime = Time.time - startTime;
        int min = ((int)stageTime) / 60;
        time.text = min + ":" + (stageTime % 60).ToString("00.0");
        health.text = "Health: " + stats.health.ToString("0");
        laps.text = "Lap " + stats.currentLap.ToString() + " OF " + 3;
        shield.text = "Shield: " + stats.shield.ToString() + " %";
        int speed = (int)player.GetComponent<Rigidbody>().transform.InverseTransformDirection(player.GetComponent<Rigidbody>().velocity).z;
        speed /= 5;
        vel.text = "Vel.: " + speed.ToString() + " km/h";
    }
}
