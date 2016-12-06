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
        
        print(collision.gameObject.tag.ToString());
        // TODO: Destroy collision.object if life < 0
        //Destroy(collision.gameObject.transform.parent.gameObject, obj.duration);


        GameObject obj = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
        Destroy(obj, ps.duration);
        AudioSource.PlayClipAtPoint(sound, transform.position, 1f);
        Destroy(gameObject);

        // TODO: change vars of object (if gameObject type is a ship)
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Ship>().life--;
        }
        else
        {
        }

    }
}
