using UnityEngine;
using System.Collections;

public static class Constants
{

    public static string[] nameShips = new string[2] { "Feisar", "Millenium Falcon" };

    public static int CountDown = 3;
    public static float impactDamage = 30f;
    public static float[] collisionDamage = new float[2] {0.2f, 0.2f};
    public static float[] speedShips = new float[2] { 136f, 156f };

    public static float shootDelay = 0.5f;

    public static string[] nameScenes = new string[2] { "Mapa 1", "Mapa 2"};
    public static string menuScene = "Menu Scene";

    public struct ShipInfo
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;

        public ShipInfo(string _name, Vector3 pos, Quaternion rot)
        {
            name = _name;
            position = pos;
            rotation = rot;
        }
    }

    public struct sceneInit
    {
        public ShipInfo[] ship;
        public int totalLaps;

        public sceneInit(int laps, ShipInfo ship1, ShipInfo ship2)
        {
            totalLaps = laps;
            ship = new ShipInfo[2];
            ship[0] = ship1;
            ship[1] = ship2;
        }
    }

    public static sceneInit[] scenes = new sceneInit[2]{
        new sceneInit(6,
            new ShipInfo(nameShips[0],new Vector3(2626.3f, 1627.5f, 5797.0f), Quaternion.Euler(new Vector3(-0.414f, -174.242f, 0.837f))),
            new ShipInfo(nameShips[1],new Vector3(2629.125f, 1625.27f, 5827.995f), Quaternion.Euler(-0.4f, -174.287f, 0.965f))
        ),
        new sceneInit(3,
            new ShipInfo(nameShips[0],new Vector3(3306.004f, 3663.007f, -1834.989f), Quaternion.Euler(new Vector3(-2.84f, -116.317f, -9.745001f))),
            new ShipInfo(nameShips[1],new Vector3(3324.0f, 3668.0f, -1846.0f), Quaternion.Euler(new Vector3(-2.0f, -114.5f, -10.0f)))
        )
    };



}
