using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public float velocity = 50000.0f;
    public GameObject shoot;
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
            GameObject obj = (GameObject)Instantiate(shoot, transform.position, transform.rotation);
            obj.transform.parent = transform;

            GameObject objProjectile = obj.transform.FindChild("Projectile").gameObject;
            objProjectile.GetComponent<Impact>().shipName = name;
            
            Destroy(obj, 2.5f);

            Rigidbody tmpRigidBody = objProjectile.GetComponent<Rigidbody>();
            tmpRigidBody.transform.Rotate(objProjectile.transform.eulerAngles);
            tmpRigidBody.AddForce(transform.forward * (velocity+gameObject.GetComponent<Ship>().speed*100));

            lastTime = 0.5f;
        }
    }

}
