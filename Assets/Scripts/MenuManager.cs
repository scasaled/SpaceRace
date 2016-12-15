﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Menu CurrentMenu;
    public Menu MapSelectionMenu;

    public List<Ship> ships = new List<Ship>();
    public List<Transform> bars = new List<Transform>();

    public static int selectedShip;

    private struct bar
    {
        public Text text;
        public Transform transfromBar;
        public bar(Text _text, Transform _transform)
        {
            text = _text;
            transfromBar = _transform;
        }
    }

    private struct props
    {
        public float maxSpeed, acceleration, rotationSpeed, grip;

    }

    private props maxs;
    private List<bar> barsInfo;

    // Use this for initialization
    void Start()
    {
        barsInfo = new List<bar>();
        foreach (Transform tr in bars)
            barsInfo.Add(
                new bar(tr.FindChild("Text").GetComponent<Text>(),
                tr.FindChild("Bar").GetComponent<Transform>())
                );

        maxs = new props();

        maxs.maxSpeed = 156f;

        foreach (Ship ship in ships)
        {
            if (maxs.acceleration < ship.acceleration) maxs.acceleration = ship.acceleration;
            if (maxs.rotationSpeed < ship.rotationSpeed) maxs.rotationSpeed = ship.rotationSpeed;
            if (maxs.grip < ship.maneig) maxs.grip = ship.maneig;
        }
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
        float speed = ((ship == 1) ? 136f : 156f);
        float ratio = Mathf.Max(0, speed / maxs.maxSpeed);
        barsInfo[0].text.text = speed.ToString("0");
        barsInfo[0].transfromBar.localScale = new Vector3(ratio, 1, 1);

        ratio = Mathf.Max(0, ships[ship - 1].acceleration / maxs.acceleration);
        barsInfo[1].text.text = ships[ship - 1].acceleration.ToString();
        barsInfo[1].transfromBar.localScale = new Vector3(ratio, 1, 1);

        ratio = Mathf.Max(0, ships[ship - 1].rotationSpeed / maxs.rotationSpeed);
        barsInfo[2].text.text = ships[ship - 1].rotationSpeed.ToString();
        barsInfo[2].transfromBar.localScale = new Vector3(ratio, 1, 1);

        ratio = Mathf.Max(0, ships[ship - 1].maneig / maxs.grip);
        barsInfo[3].text.text = ships[ship - 1].maneig.ToString();
        barsInfo[3].transfromBar.localScale = new Vector3(ratio, 1, 1);

        selectedShip = ship;
    }

    public void MapSelection(int map)
    {
        
        if (map == 1) SceneManager.LoadScene("Scene 2");
        else if (map == 1) SceneManager.LoadScene("Scene 3");
    }
}
