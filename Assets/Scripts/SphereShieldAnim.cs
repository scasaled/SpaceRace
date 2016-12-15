using UnityEngine;
using System.Collections;

public class SphereShieldAnim : MonoBehaviour {

    private float time;
    private float rot;
    private bool trigger;

	// Use this for initialization
	void Start () {
        time = Time.deltaTime;
        rot = 4.0f;
        trigger = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.up* Mathf.Sin(time) / 10.0f;
        transform.Rotate(new Vector3(0.0f,rot,0.0f));
        time += Time.deltaTime*3;
	}

    public bool isTrigger()
    {
        return trigger;
    }

    public void setTrigger(bool t)
    {
        trigger = t;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy") trigger = true;
    }
}
