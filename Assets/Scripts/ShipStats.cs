using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipStats : MonoBehaviour {

    public GameObject ship;

    public float maxHealth = 100f;
    public float health = 100f;
    public int currentLap = 0;
    public int currentPosition = 1;
    public int shield = 100;

    public float startTime = 0;
    public float stageTime = 0;

    public List<float> lapsTime;
    private float lapStartTime;

	// Use this for initialization
	void Start () {
        lapStartTime = 0f;
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        stageTime = Time.time - startTime;
	}

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 100.0f)
        {
            ship.GetComponent<Ship>().tpShip();
        }
    }

    public void lapPass()
    {
        currentLap++;
        lapsTime.Add(stageTime - lapStartTime);
        lapStartTime = stageTime;



    }
}
