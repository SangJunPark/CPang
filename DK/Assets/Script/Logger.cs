using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : ILoopable {

    public static Logger MainLog = new Logger();
    private List<string> mainlogText = new List<string>();

    public static void Instantiate()
    {
        MainLoopable.GetInstance().RegisterLoop(MainLog);
    }

    public static void Log(string log)
    {
        MainLog.log(log);
    }

    public static void Log(System.Exception e)
    {
        MainLog.log(e.StackTrace);
    }

    private void log(string log)
    {
        mainlogText.Add(log);
    }

	public void Start () {
		
	}
	
	public void Update () {
        System.IO.File.WriteAllLines("Log.txt", new List<string>(mainlogText).ToArray());
        //Debug.Log(mainlogText.ToArray());
	}

    public void OnApplicationQuit()
    {

    }
}
