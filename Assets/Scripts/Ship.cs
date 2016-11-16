using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public float speed;
	public float maxSpeed;
	public float acceleration;
	public float rotationSpeed;

	private float maxRot = 15f;
	private Rigidbody rb;
	private Quaternion originalRotation;

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
			rb.rotation = Quaternion.Lerp (rb.rotation, 
				new Quaternion (originalRotation.x, rb.rotation.y, originalRotation.z, originalRotation.w), 
				Time.deltaTime * rotationSpeed * 0.05f);
		} else {
			float angle = ClampAngle (rb.transform.eulerAngles.z, -maxRot, maxRot);
			float signe = Input.GetKey (KeyCode.RightArrow) ? 1f : -1f;
			rb.transform.Rotate (0f, signe * rotationSpeed * Time.deltaTime, (Mathf.Abs (angle) < maxRot) ? -(signe) * rotationSpeed * Time.deltaTime : 0f);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			if (speed < maxSpeed)
				speed = Mathf.Min (maxSpeed, speed + acceleration * Time.deltaTime);
		} else speed = Mathf.Max (0f, speed - acceleration * Time.deltaTime);
		rb.AddForce (transform.forward*speed);
	}

	float ClampAngle (float angle, float min, float max)
	{
		if (angle < 90 || angle > 270) {
			if (angle > 180)
				angle -= 360; 
			if (max > 180)
				max -= 360;
			if (min > 180)
				min -= 360;
		}
		angle = Mathf.Clamp (angle, min, max);
		return angle;
	}
}