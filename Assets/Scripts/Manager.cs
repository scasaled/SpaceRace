using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject panel;

    void Update()
    {
        if (Input.GetKey(KeyCode.P)) PauseGame();
    }
    
    void enableScripts(bool enabled)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            foreach (MonoBehaviour script in go.GetComponents<MonoBehaviour>())
                script.enabled = enabled;

        foreach (MonoBehaviour script in GameObject.FindGameObjectWithTag("Player").GetComponents<MonoBehaviour>())
            script.enabled = enabled;
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void menuReturn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Constants.menuScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
