using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Levitacio : MonoBehaviour {

    public float separacio;
    public float gravetat;
    public float dampFactor;
    public float maneig;

    private Rigidbody rb;
    private BoxCollider bc;
    private float ratioSeparacio;
    private float F;
    private float rigidesa;
    private float damp;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        separacio = 7.0f;
        gravetat = 980.0f;
        dampFactor = 200.0f;
        maneig = 2.0f;
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 posNau = transform.position;
        Vector3 midaNau = bc.size;

        //Els 4 punts on estaran els raycast
        List<Vector3> rayPoints = new List<Vector3>();
        rayPoints.Add(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y/2.0f, midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y/2.0f, midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y/2.0f, -midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y/2.0f, -midaNau.z / 2.5f));

        bool vola = true;

        for (int i = 0; i < rayPoints.Count; ++i)
        {
            RaycastHit hit;
            if (Physics.Raycast(rayPoints[i], -transform.up, out hit, separacio) && hit.distance <= separacio)
            {
                ratioSeparacio = ((separacio - hit.distance) / separacio);
                rigidesa = rb.mass * gravetat / rayPoints.Count;
                if (dampFactor != 0) damp = rigidesa / dampFactor;
                else damp = rigidesa / 1000.0f;                                                 //Valor per defecte
                F = rigidesa * ratioSeparacio - damp * rb.GetPointVelocity(rayPoints[i]).y;
                rb.AddForceAtPosition(F * transform.up, rayPoints[i]);
            }
        }

        //Això és perquè baixi si esta volant
        rb.AddForce(350 * -transform.up, ForceMode.Acceleration);
        
        //A més maneig, més lent anirà la nau, però més adherencia tindrá. (Si es vol conservar la velocitat, cal augmentar-la manualment)
        Vector3 vel = rb.velocity;
        vel.y = 0.0f;
        rb.AddForce(vel * -maneig, ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward, ForceMode.Acceleration);
    }
}

