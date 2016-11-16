using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{

	public float speed;
	public float rotationSpeed;

	private float maxRot = 15f;
	private Rigidbody rb;
	private Quaternion originalRotation;

	private float lastTime;

	// Use this for initialization
	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		originalRotation = rb.rotation;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.RightArrow) == Input.GetKey (KeyCode.LeftArrow)) {
			rb.rotation = Quaternion.Lerp(rb.rotation, 
				new Quaternion (originalRotation.x, rb.rotation.y, originalRotation.z, originalRotation.w), 
				Time.deltaTime * rotationSpeed * 0.1f);
		} else {
			float signe = Input.GetKey (KeyCode.RightArrow) ? 1 : -1;	
			rb.angularVelocity = new Vector3 (0.0f, signe*rotationSpeed * Time.deltaTime, -(signe)*rotationSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.UpArrow))
			rb.velocity += transform.forward * speed;
	}
}
