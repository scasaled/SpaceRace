using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Levitacio : MonoBehaviour {

    public float separacio;
    public float gravetat;
    public float dampFactor;

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
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 posNau = transform.position;
        Vector3 midaNau = bc.size;

        //Els 4 punts on estaran els raycast
        List<Vector3> rayPoints = new List<Vector3>();
        /*Debug.DrawLine(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y/2.0f, midaNau.z / 2.5f), transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y / 2.0f + 10.0f, midaNau.z / 2.5f));
        Debug.DrawLine(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y / 2.0f, midaNau.z / 2.5f), transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y / 2.0f + 10.0f, midaNau.z / 2.5f));
        Debug.DrawLine(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y / 2.0f, -midaNau.z / 2.5f), transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y / 2.0f + 10.0f, -midaNau.z / 2.5f));
        Debug.DrawLine(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y / 2.0f, -midaNau.z / 2.5f), transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y / 2.0f + 10.0f, -midaNau.z / 2.5f));*/
        rayPoints.Add(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y/2.0f, midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y/2.0f, midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y/2.0f, -midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y/2.0f, -midaNau.z / 2.5f));

        for (int i = 0; i < rayPoints.Count; ++i)
        {
            RaycastHit hit;
            if (Physics.Raycast(rayPoints[i], -transform.up, out hit, 100.0f))
            {
                ratioSeparacio = ((separacio - hit.distance) / separacio);
                if (gravetat != 0) rigidesa = rb.mass * gravetat / rayPoints.Count;
                else rigidesa = rb.mass * 980 / rayPoints.Count;                                //Valor per defecte
                if (dampFactor != 0) damp = rigidesa / dampFactor;
                else damp = rigidesa / 1000.0f;                                                 //Valor per defecte
                F = rigidesa * ratioSeparacio - damp * rb.GetPointVelocity(rayPoints[i]).y;
                rb.AddForceAtPosition(F*transform.up,rayPoints[i]);

                Debug.DrawLine(rayPoints[i], hit.point);


                //Versio anterior

                /*idealPos = transform.position + ((separacio - hit.distance) * transform.up);
                Vector3 F = (idealPos - transform.position);
                rb.AddForceAtPosition(F * 15, rayPoints[i]);*/
            }
        }
    }
}

