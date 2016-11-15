using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Levitacio : MonoBehaviour {
    
    private Rigidbody rb;
    private BoxCollider bc;
    private Vector3 idealPos;
    public float separacio;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        idealPos = transform.position;
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
                idealPos = transform.position + ((separacio - hit.distance) * transform.up);
                Vector3 F = (idealPos - transform.position);
                rb.AddForceAtPosition(F*15,rayPoints[i]);

                Debug.DrawLine(rayPoints[i], hit.point);

                //mantenim la nau verticalment al terra
                /*Quaternion rot = transform.rotation;
                if (transform.up != hit.normal.normalized)
                {
                    Vector3 x = Vector3.Cross(transform.up, hit.normal.normalized);
                    float theta = Mathf.Asin(x.magnitude);
                    Vector3 w = x.normalized * theta / Time.fixedDeltaTime;
                    Quaternion q = transform.rotation * rb.inertiaTensorRotation;
                    Vector3 T = q * Vector3.Scale(rb.inertiaTensor, (Quaternion.Inverse(q) * w));
                    rb.AddTorque(T);
                }*/
            }
        }
    }
}

