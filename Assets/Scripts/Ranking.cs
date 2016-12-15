﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Ranking : MonoBehaviour {

    public List<Transform> ships;
    static public GameObject cam;

    private struct Rank
    {
        public string ship;
        public Rank(string s)
        {
            ship = s;
        }
    };


    static private Text posPlayer;
    static private Text timePlayer;
    static private Text ship1;
    static private Text ship2;
    static private Text ship3;
    static private Text[] shipsText = new Text[3];
    static private List<Rank> results = new List<Rank>();

    // Use this for initialization
    void Start () {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("Ranking");
        posPlayer = canvasObject.transform.FindChild("Canvas/Panel/PosPlayer").GetComponent<Text>();
        timePlayer = canvasObject.transform.FindChild("Canvas/Panel/TimePlayer").GetComponent<Text>();
        shipsText[0] = canvasObject.transform.FindChild("Canvas/Panel/Ship1").GetComponent<Text>();
        shipsText[1] = canvasObject.transform.FindChild("Canvas/Panel/Ship2").GetComponent<Text>();
        shipsText[2] = canvasObject.transform.FindChild("Canvas/Panel/Ship3").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    static public int calculatePos(GameObject id)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int auxPos = 1;
        int currLapP = id.GetComponent<ShipStats>().CurrentLap;
        int indLapP = id.GetComponent<Ship>().WPindexLapPointer;
        if (id.tag != player.tag)
        {
            if (player.GetComponent<ShipStats>().CurrentLap > currLapP) ++auxPos;
            else if (player.GetComponent<ShipStats>().CurrentLap == currLapP && player.GetComponent<Ship>().WPindexLapPointer > indLapP) ++auxPos;
        }
        foreach (GameObject enem in enemies)
        {
            if (enem.name != id.name)
            {
                if (enem.GetComponent<ShipStats>().CurrentLap > currLapP) ++auxPos;
                else if (enem.GetComponent<ShipStats>().CurrentLap == currLapP && enem.GetComponent<Ship>().WPindexLapPointer > indLapP) ++auxPos;
            }
        }

        return auxPos;
    }

    static public void addShip(GameObject ship)
    {
        print("Ranking " + results.Count);
        Rank r = new Rank(ship.name);
        results.Add(r);
        if (ship.tag == "Player")
        {
            int min = ((int)ship.GetComponent<ShipStats>().StageTime) / 60;
            string res = min + ":" + (ship.GetComponent<ShipStats>().StageTime % 60).ToString("00.0");
            timePlayer.text = res;
            posPlayer.text = results.Count.ToString();
        }
    }

    static public void active()
    {
        GameObject.FindGameObjectWithTag("Ranking").transform.FindChild("Camera").gameObject.SetActive(true);
        updateInfo();
    }

    static private void updateInfo()
    {
        int i = 0;
        foreach (Rank r in results)
        {
            shipsText[i].text = r.ship;
            ++i;
        }
    }
}