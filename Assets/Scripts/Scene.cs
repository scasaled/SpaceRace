using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {

    private int totalLaps;
    private int totalShips;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    public int TotalLaps
    {
        get { return totalLaps; }
        set { totalLaps = value; }
    }

    public int TotalShips
    {
        get { return totalShips; }
        set { totalShips = value; }
    }
}
