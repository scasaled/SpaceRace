using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAShip : Ship
{
    private bool accelState;
    private bool triggered;
    private float dirNum;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        Accell();
        waypoint = waypoints[WPindexPointer];
        waypointLap = waypointsLap[WPindexLapPointer];
    }

    void Accell()
    {
        if (accelState == false)
        {
            accelState = true;
        }

        //Calcul Levitacio
        Vector3 midaNau = bc.size;

        //Els 4 punts on estaran els raycast
        List<Vector3> rayPoints = new List<Vector3>();
        rayPoints.Add(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y / 2.0f, midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y / 2.0f, midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(midaNau.x / 2.5f, -midaNau.y / 2.0f, -midaNau.z / 2.5f));
        rayPoints.Add(transform.TransformPoint(-midaNau.x / 2.5f, -midaNau.y / 2.0f, -midaNau.z / 2.5f));

        bool vola = true;

        for (int i = 0; i < rayPoints.Count; ++i)
        {
            RaycastHit hit;
            if (Physics.Raycast(rayPoints[i], -transform.up, out hit, 30) && hit.transform.tag != "SpeedBoost")
            {
                ratioSeparacio = ((separacio - hit.distance) / separacio);
                rigidesa = rb.mass * gravetat / rayPoints.Count;
                if (dampFactor != 0) damp = rigidesa / dampFactor;
                else damp = rigidesa / 1000.0f;                                                //Valor per defecte
                F = rigidesa * ratioSeparacio - damp * rb.GetPointVelocity(rayPoints[i]).y;
                rb.AddForceAtPosition(F * transform.up, rayPoints[i]);
                vola = false;

                Debug.DrawLine(rayPoints[i], hit.point);
            }
        }

        //Això és perquè baixi si esta volant
        if (vola)
        {
            rb.AddForce(90.0f * -transform.up, ForceMode.Acceleration);
            respawn += Time.deltaTime;
            if (respawn > 2.5f) tpShipIA();
        }
        else respawn = 0.0f;

        //A més maneig, més lent anirà la nau, però més adherencia tindrá. (Si es vol conservar la velocitat, cal augmentar-la manualment)
        Vector3 vel = rb.velocity;
        vel.y = 0.0f;
        rb.AddForce(vel * -maneig, ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward, ForceMode.Acceleration);

        if (waypoint)
        {
            dirNum = AngleDir(transform.forward, waypoint.position - transform.position, transform.up);
            if (dirNum == 1.0f)
            {
                girY = Mathf.Lerp(girY, rotationSpeed, Time.deltaTime * 6);
                girZ = Mathf.Lerp(girZ, 25.0f, Time.deltaTime * 3);
            }
            else if (dirNum == -1.0f)
            {
                girY = Mathf.Lerp(girY, -rotationSpeed, Time.deltaTime * 6);
                girZ = Mathf.Lerp(girZ, -25.0f, Time.deltaTime * 3);
            }
            else
            {
                rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
                girY = Mathf.Lerp(girY, 0.0f, Time.deltaTime * 8);
                girZ = Mathf.Lerp(girZ, 0.0f, Time.deltaTime * 3);
            }

            transform.Rotate(0.0f, girY * Time.deltaTime, 0.0f);
            controller.transform.rotation = Quaternion.Euler(controller.transform.eulerAngles.x, controller.transform.eulerAngles.y, -transform.eulerAngles.z + girZ);
        }
        if (speed < maxSpeed) speed += acceleration * Time.deltaTime;
        else if (speed > maxSpeed) speed -= acceleration * Time.deltaTime;
        rb.AddForce(transform.forward * speed);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && (waypoint != null) && waypoint.name == other.gameObject.name)
        {
            lastWP = other.transform;
            WPindexPointer++;
            if (WPindexPointer >= waypoints.Count)
            {
                WPindexPointer = 0;
            }
            triggered = true;
        }

        contadorLapsEnter(other);
        if (other.gameObject.tag == "SpeedBoost")
        {
            print("entra");
            speed = maxSpeed * 1.4f;
            print(speed);
            rb.AddForce(transform.forward * speed);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (triggered && other.gameObject.GetComponent<Transform>().name == lastWP.name)
        {
            triggered = false;
        }

        contadorLapsExit(other);
    }

    public void tpShipIA()
    {
        base.tpShip(lastWP.position, lastWP.rotation);
    }

    public override void Damage(float healthDamage, float shieldDamage)
    {
        base.Damage(healthDamage, shieldDamage);
        if (stats.Health == 0f) tpShipIA();
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.5f)
        {
            return 1f;
        }
        else if (dir < -0.5f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
}
