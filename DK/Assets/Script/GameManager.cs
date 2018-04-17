using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Camera uiCamera;
    public Text loadingText;

    public float dx = 50f;
    public float dz = 50f;
    public float my = 0.23f;
    public float cutoff = 1.8f;
    public float mul = 1;

    public static float Sdx = 50;
    public static float Sdz = 50;
    public static float Smy = 0.23f;
    public static float Scutoff = 1.8f;
    public static float Smul = 1f;

    private MainLoopable main;

    public void StartPlayer(Vector3 pos)
    {
        GameObject.Destroy(uiCamera);
        GameObject.Destroy(loadingText);
        GameObject go = Instantiate(Resources.Load("Player"), pos, Quaternion.identity) as GameObject;
        go.transform.position = pos;
    }
	// Use this for initialization
	void Start () {

        FileManager.Instance.RegisterFiles();
        BlockRegistry.Initialize();
        Block b = new Block(false);

        TextureAtlas.Instance.CreateAtlas();
        MainLoopable.Instantiate();
        main = MainLoopable.GetInstance();
        main.Start();
    }

    // Update is called once per frame
    void Update () {
        //Sdx = dx;
        //Sdz = dz;
        //Smy = my;
        //Scutoff = cutoff;
        //Smul = mul;
        main.Update();
	}

    void OnApplicationQuit()
    {
        main.OnApplicationQuit();
    }
}
