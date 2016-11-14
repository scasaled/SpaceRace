using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{

    public float speed = 5.0f;

    private float maxRot = 15f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        float angle = ClampAngle(gameObject.transform.eulerAngles.z, -maxRot, maxRot);
        Debug.Log(angle);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
            if (angle > -maxRot) gameObject.transform.Rotate(0.0f, 0.0f, -speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
            if (angle < maxRot) gameObject.transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
            gameObject.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    float ClampAngle(float angle, float min, float max)
    {

        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        return angle;
    }
}
