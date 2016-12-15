using UnityEngine;
using System.Collections;

public class MoureCamera : MonoBehaviour {

    public GameObject nau;

    private Vector3 offset;

    // Use this for initialization
    void Start () {
        nau = GameObject.Find("Feisar");
        offset = transform.position - nau.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = nau.transform.position + offset;
    }
}
