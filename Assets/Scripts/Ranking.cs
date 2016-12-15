using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ranking : MonoBehaviour {

    public List<Transform> ships;

    private struct Rank
    {
        string ship;
        float time;
    }

	// Use this for initialization
	void Start () {
        Dictionary<int, Rank> ranking = new Dictionary<int, Rank>();

    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
