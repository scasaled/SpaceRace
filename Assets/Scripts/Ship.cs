using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour
{

    public GameObject sparks;
    public AudioClip sparksSound;

    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float rotationSpeed;
    
    protected float maxRot = 15f;
	protected Rigidbody rb;
    protected Quaternion originalRotation;
    protected float girY;
    protected float girZ;

    public float separacio;
    public float gravetat;
    public float dampFactor;
    public float maneig;

    protected BoxCollider bc;
    protected float ratioSeparacio;
    protected float F;
    protected float rigidesa;
    protected float damp;
    protected Vector3 norm;
    protected GameObject controller;

    protected ShipStats stats;

    public List<Transform> waypoints = new List<Transform>();
    protected Transform waypoint;

    protected Transform lastWP;
    protected int WPindexPointer;

    // Use this for initialization
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody> ();
        originalRotation = rb.rotation;
        girZ = 0.0f;
        girY = 0.0f;

        stats = GetComponent<ShipStats>();
        controller = transform.Find("GameObject").gameObject;
    }

    // Update is called once per frame
    
    public virtual void Update()
    {
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Player")
        {
            GameObject obj = (GameObject)Instantiate(sparks, collision.transform.position, collision.transform.rotation);
            Destroy(obj, obj.GetComponent<ParticleSystem>().duration);
            AudioSource.PlayClipAtPoint(sparksSound, transform.position, 1f);
            //stats.Damage(0.05f);
        }
    }
}