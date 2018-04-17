using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVMap {

    private static Dictionary<int, UVMap> ms_Maps = new Dictionary<int, UVMap>();
    public string m_name;
    public Vector2[] m_uvMap;

    public UVMap(string name, Vector2[] uvMap)
    {
        m_name = name;
        m_uvMap = uvMap;
    }

    public void Register()
    {
        ms_Maps.Add(m_name.GetHashCode(), this);
    }

    public static UVMap GetUVMap(string name)
    {
        if(!string.IsNullOrEmpty(name))
        {
            int key = name.GetHashCode();
            if (ms_Maps.ContainsKey(key))
                return ms_Maps[key];
        }

        Debug.LogError("GetUVMap cant find name of image : " + name);

        //List<string> names = new List<string>();
        //foreach(var m in ms_Maps.Keys)
        //{
        //    names.Add(m + "!=" + name);
        //}

        //System.IO.File.WriteAllLines("names.txt", names.ToArray());
        return null;
    }
}
