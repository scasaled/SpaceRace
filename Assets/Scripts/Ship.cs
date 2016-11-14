using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{

    public float speed;
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
		print(rb.transform.rotation.y);
		if (Input.GetKey (KeyCode.RightArrow) == Input.GetKey (KeyCode.LeftArrow))
			rb.angularVelocity = new Vector3 (0.0f, 0.0f, 0.0f);
		else {	
			if (Input.GetKey (KeyCode.RightArrow))
				rb.angularVelocity = new Vector3 (0.0f, rotationSpeed * Time.deltaTime, 0.0f);
			else if (Input.GetKey (KeyCode.LeftArrow))
				rb.angularVelocity = new Vector3 (0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			rb.velocity += transform.forward * speed;
			//gameObject.transform.Translate (0.0f, 0.0f, speed * Time.deltaTime);
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
