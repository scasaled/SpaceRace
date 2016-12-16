using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    public GameObject explosion;
    public GameObject sparks;
    private bool isSparksPlaying;

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

    protected List<Transform> waypoints = new List<Transform>();
    protected List<Transform> waypointsLap = new List<Transform>();
    protected Transform waypoint;
    protected Transform waypointLap;
    public Transform waypointsList;
    public Transform waypointsLapList;

    protected Transform lastWP;
    protected Transform lastWPLap;
    protected int WPindexPointer;
    public int WPindexLapPointer;

    private bool triggered;
    private bool first;
    public float respawn;
    protected Camera cam;
    protected Vector3 cameraInitPos;

    public int actualPos;

    // Use this for initialization
    public virtual void Start()
    {
        waypointsLapList = GameObject.Find("WP Laps").transform;
        cam = GetComponentInChildren<Camera>();
        if (cam != null) cameraInitPos = cam.transform.position;
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        originalRotation = rb.rotation;
        girZ = 0.0f;
        girY = 0.0f;
        first = true;
        WPindexPointer = 0;
        WPindexLapPointer = 0;
        respawn = 0.0f;
        controller = transform.Find("Controller").gameObject;

        stats = GetComponent<ShipStats>();
        
        if (waypointsList != null)
        {
            foreach (Transform wp in waypointsList) waypoints.Add(wp);
        }
        foreach (Transform wp in waypointsLapList) waypointsLap.Add(wp);
        lastWPLap = waypointsLap[WPindexLapPointer];

        isSparksPlaying = false;
        sparks.GetComponent<AudioSource>().time = 4f;
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Player")
        {
            if (!isSparksPlaying)
            {
                GameObject obj = (GameObject)Instantiate(sparks, collision.transform.position, collision.transform.rotation);
                float duration = obj.GetComponent<ParticleSystem>().duration;
                Destroy(obj, duration);
                Invoke("SparksFinished", duration);
                isSparksPlaying = true;
            }
            Damage(Constants.collisionDamage[0], Constants.collisionDamage[1]);
        }
    }

    private void SparksFinished()
    {
        isSparksPlaying = false;
    }

    public void contadorLapsEnter(Collider other)
    {
        if (!triggered && waypointLap != null && waypointLap.name == other.gameObject.name)
        {
            WPindexLapPointer++;
            if (WPindexLapPointer >= waypointsLap.Count) WPindexLapPointer = 0;

            if (WPindexLapPointer == 1 && !first) LapPass();
            else if (first) first = false;
            lastWPLap = other.transform;

            triggered = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) player.GetComponent<Ship>().actualPos = Ranking.calculatePos(player);
        }
    }

    public void contadorLapsExit(Collider other)
    {
        if (triggered && other.gameObject.GetComponent<Transform>().name == lastWPLap.name)
        {
            triggered = false;
        }
    }

    public virtual void Damage(float damage)
    {
        if (stats.Shield >= damage)
            stats.Shield -= damage;
        else
        {
            stats.Shield = 0f;
            stats.Health -= damage - stats.Shield;
        }
    }

    public virtual void Damage(float healthDamage, float shieldDamage)
    {
        stats.Health -= healthDamage;
        stats.Shield -= shieldDamage;
    }

    public virtual void LapPass()
    {
        int scene = SceneManager.GetActiveScene().name == Constants.nameScenes[0] ? 0 : 1;
        stats.LapPass();
        if (stats.CurrentLap == Constants.scenes[scene].totalLaps)
        {
            if (gameObject.tag != "Player")
            {
                Ranking.addShip(gameObject);
                Destroy(gameObject);
            }
            else
            {
                Ranking.addShip(gameObject);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] aux = new GameObject[enemies.Length + 1];
                foreach (GameObject enem in enemies)
                {
                    if (enem != null)
                    {
                        aux[Ranking.calculatePos(enem)-1] = enem;
                    }
                }
                foreach (GameObject trol in aux)
                {
                    if (trol != null)
                    {
                        Ranking.addShip(trol);
                        Destroy(trol);
                    }
                }
                Ranking.active();
                Destroy(gameObject);
            }
        }
    }

    protected void tpShip(Vector3 position, Quaternion rotation)
    {
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        GameObject parent = new GameObject("DestroyExplosion");

        for (int i = 0; i < 4; ++i)
        {
            GameObject obj = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            obj.transform.parent = parent.transform;
        }

        Destroy(parent, ps.duration);

        rb.isKinematic = true;
        transform.position = position;
        transform.rotation = rotation;
        rb.isKinematic = false;

        respawn = 0.0f;
        stats.restartHealth();
        stats.restartShield();
    }

    protected void triggerSphereShield(Collider other)
    {
        if (other.gameObject.tag == "Sphere Shield")
        {
            if (stats.Shield < 80.0f) stats.Shield += 20.0f;
            else stats.Shield = 100.0f;
        }
    }
}