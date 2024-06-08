using System;
using System.Collections.Generic;

public class ApplicationData
{    
    private static ApplicationData _instance;

    public static ApplicationData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ApplicationData();
            }
            return _instance;
        }
    }

    public List<Resource> GameResource = new List<Resource>();

    public bool MainFirstStart = true;
    public bool ChestFirstStart = true;
    public bool SpaceShipFirstStart = true;

    public float GameVolume = 1f;
    public bool GameIsMute = false;

    public bool FirstStart()
    {
        var first = false;

        if (MainFirstStart) first = MainFirstStart;
        if (ChestFirstStart) first = ChestFirstStart;
        if (SpaceShipFirstStart) first = SpaceShipFirstStart;

        return first;
    }
}

[Serializable]
public class Resource
{
    public string Name;
    public int Count;
}