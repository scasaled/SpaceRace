using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public float velocity = 500.0f;

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
            GameObject obj = (GameObject)Instantiate(projectile, transform.position + new Vector3(0f,0f,0f), transform.rotation);

            Rigidbody tmpRigidBody = obj.GetComponent<Rigidbody>();
            tmpRigidBody.transform.Rotate(projectile.transform.eulerAngles);
            tmpRigidBody.AddForce(transform.forward * velocity);

            lastTime = 0.5f;
        }
    }

}
