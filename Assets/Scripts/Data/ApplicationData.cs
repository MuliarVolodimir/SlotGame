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
}

[Serializable]
public class Resource
{
    public string Name;
    public int Count;
}