﻿using UnityEngine;
using System.Collections;

public class Impact : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip sound;

    public string shipName;

    private ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        ps = explosion.GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag  == "Enemy") &&
            other.name != shipName)
        {
            GameObject obj = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(sound, transform.position, 1f);
            Destroy(obj, ps.duration);
            Destroy(gameObject);

            // If it has a shield, destroy his shield, if not:
            other.gameObject.GetComponent<ShipStats>().Health-=100f;
            if (other.gameObject.GetComponent<ShipStats>().Health == 0f)
            {
                // Restart enemy from last point?
                if (other.gameObject.tag == "Player") other.gameObject.GetComponent<PlayerShip>().tpShip();
                else if (other.gameObject.tag == "Enemy") other.gameObject.GetComponent<IAShip>().tpShip();
            }
            
        }
    }
}
