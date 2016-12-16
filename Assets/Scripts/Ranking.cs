using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Ranking : MonoBehaviour {

    public List<Transform> ships;
    static public GameObject cam;
    public static int positionPlus = 0;

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
    static private Text[] shipsText = new Text[4];
    static private List<Rank> results = new List<Rank>();

    // Use this for initialization
    void Start () {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("Ranking");
        posPlayer = canvasObject.transform.FindChild("Canvas/Panel/Position/PosPlayer").GetComponent<Text>();
        timePlayer = canvasObject.transform.FindChild("Canvas/Panel/Time/TimePlayer").GetComponent<Text>();
        shipsText[0] = canvasObject.transform.FindChild("Canvas/Panel/Positions/Panel/Ships/Ship1").GetComponent<Text>();
        shipsText[1] = canvasObject.transform.FindChild("Canvas/Panel/Positions/Panel/Ships/Ship2").GetComponent<Text>();
        shipsText[2] = canvasObject.transform.FindChild("Canvas/Panel/Positions/Panel/Ships/Ship3").GetComponent<Text>();
        shipsText[3] = canvasObject.transform.FindChild("Canvas/Panel/Positions/Panel/Ships/Ship4").GetComponent<Text>();
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
        } else auxPos+= positionPlus;
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

    static public void addShip(GameObject ship, bool sumPos = false)
    {
        Rank r = new Rank(ship.name);
        results.Add(r);
        if (ship.tag == "Player")
        {
            int min = ((int)ship.GetComponent<ShipStats>().StageTime) / 60;
            string res = min + ":" + (ship.GetComponent<ShipStats>().StageTime % 60).ToString("00.0");
            timePlayer.text = res;
            posPlayer.text = results.Count.ToString();
        }

        if (sumPos) positionPlus++;
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

    public void returnMenu()
    {
        SceneManager.LoadScene(Constants.menuScene);
    }
}
