using UnityEngine;
using System.Collections;

public class Impact : MonoBehaviour
{
    public GameObject explosion;
    public string shipName;

    private float duration;

    // Use this for initialization
    void Start()
    {
        duration = explosion.GetComponent<ParticleSystem>().duration;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag  == "Enemy") &&
            other.name != shipName)
        {
            GameObject obj = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            Destroy(obj, duration);
            Destroy(gameObject);

            other.gameObject.GetComponent<Ship>().Damage(Constants.impactDamage);
        }
    }
}
