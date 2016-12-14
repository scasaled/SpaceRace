using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text text;
    private System.Timers.Timer timer;
    private int countDown = 3;

    void Start()
    {
        StartCoroutine(waitSec());
    }

    private IEnumerator waitSec()
    {
        enableScripts(false);
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<AudioSource>().Play();
        text.enabled = true;
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
        {
            countDown--;
        };
        timer.Start();
    }

    void Update()
    {
        if (countDown >= 0)
            text.text = countDown.ToString();
        if (countDown == 0)
            enableScripts(true);
        else if (countDown == -1)
            text.enabled = false;
        else if (countDown == -2)
            Destroy(gameObject);
    }

    void enableScripts(bool enabled)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            foreach (MonoBehaviour script in go.GetComponents<MonoBehaviour>())
            {
                script.enabled = enabled;
            }
        }

        foreach (MonoBehaviour script in GameObject.FindGameObjectWithTag("Player").GetComponents<MonoBehaviour>())
        {
            script.enabled = enabled;
        }
    }
}
