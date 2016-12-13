using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public float velocity = 50000.0f;

    private float lastTime;

    // Use this for initialization
    void Start()
    {
        lastTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        lastTime -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && (lastTime <= 0.0f))
        {
            GameObject obj = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            obj.transform.parent = transform;

            obj.GetComponent<Impact>().shipName = name;
            Destroy(obj, 2.5f);

            Rigidbody tmpRigidBody = obj.GetComponent<Rigidbody>();
            tmpRigidBody.transform.Rotate(projectile.transform.eulerAngles);
            tmpRigidBody.AddForce(transform.forward * (velocity+gameObject.GetComponent<Ship>().speed*100));

            lastTime = 0.5f;
        }
    }

}
