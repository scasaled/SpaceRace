using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour
{
	public GameObject projectile;

	public float speed;
	public float maxSpeed;
	public float acceleration;
	public float rotationSpeed;

	private float maxRot = 15f;
	private Rigidbody rb;

	private Quaternion originalRotation;
	private float girY;
	private float girZ;

	private float lastTime;

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

	// Use this for initialization
	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		originalRotation = rb.rotation;

		bc = GetComponent<BoxCollider> ();

		lastTime = 0f;
		girZ = 0.0f;
		girY = 0.0f;
	}

	// Update is called once per frame
	void Update ()
	{
		//Calcul Levitacio

		Vector3 posNau = transform.position;
		Vector3 midaNau = bc.size;
		//Els 4 punts on estaran els raycast
		List<Vector3> rayPoints = new List<Vector3> ();
		rayPoints.Add (transform.TransformPoint (midaNau.x / 2.5f, -midaNau.y / 2.0f, midaNau.z / 2.5f));
		rayPoints.Add (transform.TransformPoint (-midaNau.x / 2.5f, -midaNau.y / 2.0f, midaNau.z / 2.5f));
		rayPoints.Add (transform.TransformPoint (midaNau.x / 2.5f, -midaNau.y / 2.0f, -midaNau.z / 2.5f));
		rayPoints.Add (transform.TransformPoint (-midaNau.x / 2.5f, -midaNau.y / 2.0f, -midaNau.z / 2.5f));

		bool vola = true;

		for (int i = 0; i < rayPoints.Count; ++i) {
			RaycastHit hit;
			if (Physics.Raycast (rayPoints [i], -transform.up, out hit, separacio) && hit.distance <= separacio) {
				norm = hit.normal;
				ratioSeparacio = ((separacio - hit.distance) / separacio);
				rigidesa = rb.mass * gravetat / rayPoints.Count;
				if (dampFactor != 0)
					damp = rigidesa / dampFactor;
				else
					damp = rigidesa / 1000.0f;                                                //Valor per defecte
				//F = rigidesa * ratioSeparacio;
				F = rigidesa * ratioSeparacio - damp * rb.GetPointVelocity (rayPoints [i]).y;
				rb.AddForceAtPosition (F * transform.up, rayPoints [i]);
				vola = false;
			}
		}

		//Això és perquè baixi si esta volant
		rb.AddForce (250.0f * -Vector3.up, ForceMode.Acceleration);

		//A més maneig, més lent anirà la nau, però més adherencia tindrá. (Si es vol conservar la velocitat, cal augmentar-la manualment)
		Vector3 vel = rb.velocity;
		vel.y = 0.0f;
		rb.AddForce (vel * -maneig, ForceMode.Acceleration);
		rb.AddRelativeForce (Vector3.forward, ForceMode.Acceleration);



		//Calcul Gir

		//float angle = ClampAngle(gameObject.transform.rotation.z, -maxRot, maxRot);

		if (Input.GetKey (KeyCode.RightArrow)) {
			girY = Mathf.Lerp (girY, 100.0f, Time.deltaTime * 6);
			girZ = Mathf.Lerp (girZ, 25.0f, Time.deltaTime * 3);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			girY = Mathf.Lerp (girY, -100.0f, Time.deltaTime * 6);
			girZ = Mathf.Lerp (girZ, -25.0f, Time.deltaTime * 3);
		} else {
			rb.angularVelocity = new Vector3 (0.0f, 0.0f, 0.0f);
			girY = Mathf.Lerp (girY, 0.0f, Time.deltaTime * 8);
			girZ = Mathf.Lerp (girZ, 0.0f, Time.deltaTime * 3);
		}

		transform.Rotate (0.0f, girY * Time.deltaTime, 0.0f);
		//transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
		//transform.Rotate(Vector3.up * girY);
		//transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);

		//controller.transform.rotation = Quaternion.Euler(controller.transform.eulerAngles.x, controller.transform.eulerAngles.y, girZ);

		/*if (Input.GetKey (KeyCode.RightArrow) == Input.GetKey (KeyCode.LeftArrow))
			rb.angularVelocity = new Vector3 (0.0f, 0.0f, 0.0f);
		else {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
            }
        }*/

		if (Input.GetKey (KeyCode.UpArrow)) {

			if (speed < maxSpeed)
				speed += acceleration * Time.deltaTime;
			rb.AddForce (transform.forward * speed);
		} else {
			if (speed > 0)
				speed -= acceleration * Time.deltaTime;
			else if (speed < 0)
				speed = 0;
			rb.AddForce (transform.forward * speed);
		}

	}
}