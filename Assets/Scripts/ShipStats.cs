﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipStats : MonoBehaviour
{
    private float maxHealth = Constants.maxHealth;
    private float health = Constants.maxHealth;
    private float maxShield = Constants.maxShield;
    private float shield = Constants.maxShield;

    private int currentLap = 0;

    private float startTime = 0;
    private float stageTime = 0;
    private float healthTime;

    public List<float> lapsTime;
    private float lapStartTime;


    // Use this for initialization
    void Start()
    {
        lapStartTime = 0f;
        startTime = healthTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - healthTime > 1f)
        {
            if (health < maxHealth) health = Mathf.Min(health + 1, maxHealth);
            healthTime = Time.time;
        }
        stageTime = Time.time - startTime;

    }

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public float MaxShield
    {
        get { return maxShield; }
    }

    public void restartHealth()
    {
        health = maxHealth;
    }
    public void restartShield()
    {
        shield = maxShield;
    }

    public float StageTime
    {
        get { return stageTime; }
    }

    public int CurrentLap
    {
        get { return currentLap; }
    }

    public float LastLapTime
    {
        get { return currentLap > 0 ? lapsTime[currentLap - 1] : 0f; }
    }

    public float Health
    {
        get { return health; }
        set { health = Mathf.Max(0f, value); }
    }
    public float Shield
    {
        get { return shield; }
        set { shield = Mathf.Max(0f, value); }
    }

    public void LapPass()
    {
        currentLap++;
        lapsTime.Add(stageTime - lapStartTime);
        lapStartTime = stageTime;
    }
}
