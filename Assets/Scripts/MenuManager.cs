using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Menu CurrentMenu;
    public Menu MapSelectionMenu;

    // Use this for initialization
    void Start()
    {
        ShowMenu(CurrentMenu);
    }

    public void ShowMenu(Menu menu)
    {
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false;

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
    }

    public void ShipSelection(int ship)
    {
        ShowMenu(MapSelectionMenu);
    }

    public void MapSelection(int map)
    {
        StartGame();
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("Scene2");

    }
}
