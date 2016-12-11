﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float acceleration;
    public float rotationSpeed;
    
    private float maxRot = 15f;
	private Rigidbody rb;
    private Quaternion originalRotation;
    private float girY;
    private float girZ;

    public float separacio;
    public float gravetat;
    public float dampFactor;
    public float maneig;
    
    private BoxCollider bc;
    private float ratioSeparacio;
    private float F;
    private float rigidesa;
    private float damp;
    private Vector3 norm;
    public GameObject controller;

    public GameObject sparks;
    public AudioClip sparksSound;
    private ShipStats stats;

    // Use this for initialization
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody> ();
        originalRotation = rb.rotation;
        girZ = 0.0f;
        girY = 0.0f;

        stats = GetComponent<ShipStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calcul Levitacio

        Vector3 posNau = transform.position;
        Vector3 midaNau = bc.size;

        //Els 4 punts on estaran els raycast
        List<Vector3> rayPoints = new List<Vector3>();
        rayPoints.Add(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y / 2.0f, midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y / 2.0f, midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y / 2.0f, -midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y / 2.0f, -midaNau.z / 2.5f));

        bool vola = true;

        for (int i = 0; i < rayPoints.Count; ++i)
        {
            RaycastHit hit;
            if (Physics.Raycast(rayPoints[i], -transform.up, out hit,30))
            {
                ratioSeparacio = ((separacio - hit.distance) / separacio);
                rigidesa = rb.mass * gravetat / rayPoints.Count;
                if (dampFactor != 0) damp = rigidesa / dampFactor;
                else damp = rigidesa / 1000.0f;                                                //Valor per defecte
                F = rigidesa * ratioSeparacio - damp * rb.GetPointVelocity(rayPoints[i]).y;
                rb.AddForceAtPosition(F * transform.up, rayPoints[i]);
                vola = false;

                Debug.DrawLine(rayPoints[i], hit.point);
            }
        }

        //Això és perquè baixi si esta volant
        if (vola) rb.AddForce(90.0f * -transform.up, ForceMode.Acceleration);

        //A més maneig, més lent anirà la nau, però més adherencia tindrá. (Si es vol conservar la velocitat, cal augmentar-la manualment)
        Vector3 vel = rb.velocity;
        vel.y = 0.0f;
        rb.AddForce(vel * -maneig, ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward, ForceMode.Acceleration);


        if (Input.GetKey(KeyCode.RightArrow))
        {
            girY = Mathf.Lerp(girY, rotationSpeed, Time.deltaTime * 6);
            girZ = Mathf.Lerp(girZ, 25.0f, Time.deltaTime * 3);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            girY = Mathf.Lerp(girY, -rotationSpeed, Time.deltaTime * 6);
            girZ = Mathf.Lerp(girZ, -25.0f, Time.deltaTime * 3);
        }
        else
        {
            rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            girY = Mathf.Lerp(girY, 0.0f, Time.deltaTime * 8);
            girZ = Mathf.Lerp(girZ, 0.0f, Time.deltaTime * 3);
        }

        transform.Rotate(0.0f, girY * Time.deltaTime, 0.0f);

        controller.transform.rotation = Quaternion.Euler(controller.transform.eulerAngles.x, controller.transform.eulerAngles.y, -transform.eulerAngles.z + girZ);

        if (Input.GetKey (KeyCode.UpArrow)) {

            if (speed < maxSpeed) speed += acceleration * Time.deltaTime;
            rb.AddForce(transform.forward * speed);
        }
        else
        {
            if (speed > 0) speed -= acceleration * Time.deltaTime;
            else if (speed < 0) speed = 0;
            rb.AddForce(transform.forward * speed);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            GameObject obj = (GameObject)Instantiate(sparks, collision.transform.position, collision.transform.rotation);
            Destroy(obj, obj.GetComponent<ParticleSystem>().duration);
            AudioSource.PlayClipAtPoint(sparksSound, transform.position, 1f);
            stats.health -= 0.05f;
        }
    }
}