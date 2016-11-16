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

    // Use this for initialization
    void Start()
    {
		rb = gameObject.GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        
		float angle = ClampAngle(gameObject.transform.rotation.z, -maxRot, maxRot);
        //print(rb.transform.rotation.y);
        //print(transform.rotation);
        if (Input.GetKey (KeyCode.RightArrow) == Input.GetKey (KeyCode.LeftArrow))
			rb.angularVelocity = new Vector3 (0.0f, 0.0f, 0.0f);
		else {
            if (Input.GetKey(KeyCode.RightArrow))
                //rb.angularVelocity = new Vector3 (0.0f, (rotationSpeed * Time.deltaTime), 0.0f);
                transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
            else if (Input.GetKey(KeyCode.LeftArrow))
                //rb.angularVelocity = new Vector3 (0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
                transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
        }
		if (Input.GetKey (KeyCode.UpArrow)) {

            //print(transform.forward);
            if (speed < maxSpeed) speed += acceleration * Time.deltaTime;
            transform.position += transform.TransformDirection(Vector3.forward) * speed;
            //rb.velocity += transform.forward * speed;
            //print(rb.velocity);
			//gameObject.transform.Translate (0.0f, 0.0f, speed * Time.deltaTime);
		}
        else
        {
            if (speed > 0) speed -= acceleration * Time.deltaTime;
            else if (speed < 0) speed = 0;
            transform.position += transform.TransformDirection(Vector3.forward) * speed;
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {
            if (angle > 180) angle -= 360; 
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        return angle;
    }
}