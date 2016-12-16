using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Init: MonoBehaviour
{
    public Text text;
    private System.Timers.Timer timer;
    private int countDown = Constants.CountDown;

    void Start()
    {
        
        GameObject stats = (GameObject)Instantiate(Resources.Load("Stats", typeof(GameObject)));
        stats.name = "Stats";

        Constants.ShipInfo shipInfo = Constants.scenes[MenuManager.selectedMap - 1].ship[MenuManager.selectedShip - 1];
        GameObject ship = (GameObject)Instantiate(Resources.Load(shipInfo.name, typeof(GameObject)), shipInfo.position, shipInfo.rotation);
        ship.name = shipInfo.name;
        
        stats.GetComponent<HUDManager>().setTotalLaps(Constants.scenes[MenuManager.selectedMap - 1].totalLaps);
        stats.GetComponent<HUDManager>().setTotalShips(GameObject.FindGameObjectsWithTag("Enemy").Length + 1);
        stats.GetComponent<HUDManager>().setCamera(ship.transform.FindChild("Camera").GetComponent<Camera>());
        ship.GetComponent<PlayerShip>().setStats(stats);

        stats.SetActive(false);
        StartCoroutine(waitSec(stats));
    }

    private IEnumerator waitSec(GameObject stats)
    {
        enableScripts(false);
        yield return new WaitForSeconds(2);
        stats.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
        text.enabled = true;
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => countDown--;
        timer.Start();
    }

    void Update()
    {
        if (countDown > 0)  text.text = countDown.ToString();
        else if (countDown == 0) {
            enableScripts(true);
            text.text = "GO!";
        }
        else if (countDown == -1) text.enabled = false;
    }

    void enableScripts(bool enabled)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            foreach (MonoBehaviour script in go.GetComponents<MonoBehaviour>())
                script.enabled = enabled;

        foreach (MonoBehaviour script in GameObject.FindGameObjectWithTag("Player").GetComponents<MonoBehaviour>())
            script.enabled = enabled;
    }
}
