using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public float velocity = 50000.0f;

    private float lastTime;
    private BoxCollider bc;

    // Use this for initialization
    void Start()
    {
        lastTime = 0.0f;
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midaNau = bc.size;
        lastTime -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && (lastTime <= 0.0f))
        {
            GameObject obj = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            obj.GetComponent<Impact>().shipName = name;

            Rigidbody tmpRigidBody = obj.GetComponent<Rigidbody>();
            tmpRigidBody.transform.Rotate(projectile.transform.eulerAngles);
            tmpRigidBody.AddForce(transform.forward * (velocity+gameObject.GetComponent<Ship>().speed*100));

            lastTime = 0.5f;
        }
    }

}
