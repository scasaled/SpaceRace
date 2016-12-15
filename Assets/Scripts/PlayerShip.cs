using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShip : Ship
{
    private HUDManager hudManager;
    private float offsetCamera = 0.0f;
    bool boost = false;
    float timer = 0.0f;

    public override void Start()
    {
        base.Start();
        updateStats();
    }

    public void setStats(GameObject stats)
    {
        hudManager = stats.GetComponent<HUDManager>();
    }

    private void updateStats()
    {
        actualPos = 3;
        hudManager.updateHealth(stats.Health, stats.MaxHealth);
        hudManager.updateShield(stats.Shield);
    }

    public override void Update()
    {
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
            if (respawn > 2.5f) tpShipPlayer();
        }
        else respawn = 0.0f;

        //A més maneig, més lent anirà la nau, però més adherencia tindrá. (Si es vol conservar la velocitat, cal augmentar-la manualment)
        Vector3 vel = rb.velocity;
        vel.y = 0.0f;
        rb.AddForce(vel * -maneig, ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward, ForceMode.Acceleration);


        if (Input.GetKey(KeyCode.RightArrow))
        {
            girY = Mathf.Lerp(girY, rotationSpeed, Time.deltaTime * 6);
            girZ = Mathf.Lerp(girZ, 30.0f, Time.deltaTime * 3);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            girY = Mathf.Lerp(girY, -rotationSpeed, Time.deltaTime * 6);
            girZ = Mathf.Lerp(girZ, -30.0f, Time.deltaTime * 3);
        }
        else
        {
            rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            girY = Mathf.Lerp(girY, 0.0f, Time.deltaTime * 8);
            girZ = Mathf.Lerp(girZ, 0.0f, Time.deltaTime * 3);
        }

        transform.Rotate(0.0f, girY * Time.deltaTime, 0.0f);

        controller.transform.rotation = Quaternion.Euler(controller.transform.eulerAngles.x, controller.transform.eulerAngles.y, -transform.eulerAngles.z + girZ);

        if (Input.GetKey(KeyCode.UpArrow))
        {

            if (speed < maxSpeed) speed += acceleration * Time.deltaTime;
            else if (speed > maxSpeed) speed -= acceleration * Time.deltaTime;
            rb.AddForce(transform.forward * speed);
            if (!boost) offsetCamera = Mathf.Lerp(offsetCamera, 100.0f, Time.deltaTime * 1.5f);
            else offsetCamera = Mathf.Lerp(offsetCamera, 200.0f, Time.deltaTime * 5.0f);
        }
        else
        {
            if (speed > 0) speed -= acceleration * Time.deltaTime;
            else if (speed < 0) speed = 0;
            rb.AddForce(transform.forward * speed);
            if (!boost) offsetCamera = Mathf.Lerp(offsetCamera, 0.0f, Time.deltaTime);
            else offsetCamera = Mathf.Lerp(offsetCamera, 200.0f, Time.deltaTime*5.0f);
        }
        waypointLap = waypointsLap[WPindexLapPointer];

        hudManager.updateTime(stats.StageTime);

        //Move camera
        if (gameObject.name == Constants.nameShips[0]) cam.transform.position = transform.TransformPoint(new Vector3(0.0f, 59.6f + (offsetCamera / 5.0f), -208.8f - offsetCamera));
        else if (gameObject.name == Constants.nameShips[1]) cam.transform.position = transform.TransformPoint(new Vector3(-3500.0f, 10479.0f + (offsetCamera * 150 / 5.0f), -23497.0f - offsetCamera * 150));

        if (boost) timer += Time.deltaTime;
        if (timer > 1.0f)
        {
            boost = false;
            timer = 0.0f;
        }
        int velocity = (int)rb.transform.InverseTransformDirection(rb.velocity).z;
        velocity /= 5;
        hudManager.updateSpeed(velocity);

        hudManager.updatePos(actualPos);
    }

    void OnTriggerEnter(Collider other)
    {
        contadorLapsEnter(other);
        if (other.gameObject.tag == "SpeedBoost")
        {
            boost = true;
            speed = maxSpeed*1.7f;
            rb.AddForce(transform.forward * speed);
        }

        if (other.gameObject.tag == "Sphere Shield")
        {
            if (stats.Shield < 80.0f) stats.Shield += 20.0f;
            else stats.Shield = 100.0f;
            hudManager.updateShield(stats.Shield);
        }
    }

    void OnTriggerExit(Collider other)
    {
        contadorLapsExit(other);
    }

    public void tpShipPlayer()
    {
        base.tpShip(lastWPLap.position, lastWPLap.rotation);
        updateStats();
    }

    public override void Damage(float healthDamage, float shieldDamage)
    {
        base.Damage(healthDamage, shieldDamage);
        hudManager.updateHealth(stats.Health, stats.MaxHealth);
        hudManager.updateShield(stats.Shield);
        if (stats.Health == 0f) tpShipPlayer();
    }

    public override void LapPass()
    {
        base.LapPass();
        hudManager.updateLapTime(stats.CurrentLap - 1, stats.LastLapTime);
        hudManager.updateLap(stats.CurrentLap + 1);
    }


}