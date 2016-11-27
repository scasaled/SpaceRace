using UnityEngine;
using System.Collections;

public class Impact : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip sound;

    private ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        ps = explosion.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: get type of shoot, and update it depending of type
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
        Destroy(obj, ps.duration);

        // TODO: Destroy collision.object if life < 0
        //Destroy(collision.gameObject.transform.parent.gameObject, obj.duration);

        AudioSource.PlayClipAtPoint(sound, transform.position, 1f);
        Destroy(gameObject);
    }
}
