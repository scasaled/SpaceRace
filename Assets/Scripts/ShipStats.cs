using UnityEngine;
using System.Collections;

public class ShipStats : MonoBehaviour {


    public GameObject ship;

    public float maxHealth = 100f;
    public float health = 100f;
    public int currentLap = 0;
    public int currentPosition = 1;
    public int shield = 100;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Damage(float damage)
    {
        health -= damage;
        if (health < 100.0f)
        {
            Debug.Log("ENTRA");
            GameObject obj = (GameObject)Instantiate(ship, transform.position + new Vector3(0f, 0f, 0f), transform.rotation);
            Destroy(gameObject);
            obj.name = "Feisar";
        }
    }
}
