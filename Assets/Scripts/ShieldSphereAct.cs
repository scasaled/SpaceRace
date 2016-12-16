using UnityEngine;
using System.Collections;

public class ShieldSphereAct : MonoBehaviour {

    private GameObject[] shieldBalls;
    private float[] timers;
    private float spawnTime = Constants.SphereSpawnTime;

	// Use this for initialization
	void Start () {
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
                timers[i] = 0.0f;
                StartCoroutine(disableBall(ball));
            }
            ++i;
        }
        for (int j = 0; j < timers.Length; ++j)
        {
            if (timers[j] < spawnTime)
                timers[j] += Time.deltaTime;
            else if (!shieldBalls[j].activeSelf)
                shieldBalls[j].SetActive(true);
        }
	}

    private IEnumerator disableBall(GameObject ball)
    {
        yield return new WaitForSeconds(0.1f);
        ball.SetActive(false);
    }
}
