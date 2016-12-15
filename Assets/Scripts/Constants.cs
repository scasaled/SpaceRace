using UnityEngine;
using System.Collections;

public static class Constants
{

    private static string[] nameShips = new string[2] { "Feisar", "Millenium Falcon" };

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

        public sceneInit(ShipInfo ship1, ShipInfo ship2)
        {
            ship = new ShipInfo[2];
            ship[0] = ship1;
            ship[1] = ship2;
        }
    }

    public static sceneInit[] scenes = new sceneInit[2]{
        new sceneInit(
            new ShipInfo(nameShips[0],new Vector3(3053f, 3667.9f, -1881.4f), new Quaternion(0f, -0.8f, -0.1f, 0.6f)),
            new ShipInfo(nameShips[1],new Vector3(3063.9f, 3665.3f, -1869.4f), new Quaternion(0.1f, -0.8f, -0.1f, 0.5f))
        ),
        new sceneInit(
            new ShipInfo(nameShips[0],new Vector3(2446.001f, 1647.003f, 5180.001f), new Quaternion(0.1f, -1.0f, 0.0f, 0.1f)),
            new ShipInfo(nameShips[1],new Vector3(2444.45f, 1647.229f, 5166.386f), new Quaternion(0.1f, -1.0f, 0.0f, 0.1f))
        )
    };



}
