using UnityEngine;
using System.Collections;

public class ShieldSphereAct : MonoBehaviour {

    private GameObject[] shieldBalls;
    private float[] timers;
    private float spawnTime;

	// Use this for initialization
	void Start () {
        spawnTime = 40.0f;
        shieldBalls = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            shieldBalls[i] = child.gameObject;
            ++i;
        }

        timers = new float[transform.childCount];
        for (int j = 0; j < timers.Length; ++j)
        {
            timers[j] = spawnTime;
        }
	}
	
	// Update is called once per frame
	void Update () {
        int i = 0;
        foreach (GameObject ball in shieldBalls)
        {
            if (ball.GetComponent<SphereShieldAnim>().isTrigger())
            {
                ball.GetComponent<SphereShieldAnim>().setTrigger(false);
                ball.SetActive(false);
                timers[i] = 0.0f;
            }
            ++i;
        }
        for (int j = 0; j < timers.Length; ++j)
        {
            if (timers[j] < spawnTime) timers[j] += Time.deltaTime;
            else if (!shieldBalls[j].activeSelf)
            {
                shieldBalls[j].SetActive(true);
            }
        }
	}
}
