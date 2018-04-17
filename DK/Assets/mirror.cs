using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror : MonoBehaviour {

    Material mat;
    [SerializeField] Camera goProCam;
    RenderTexture rt;

	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        rt = new RenderTexture(1280, 720, 24);
        goProCam.targetTexture = rt;
        mat.mainTexture = rt;
    }

    // Update is called once per frame
    void Update () {

        
        //Camera.main.Render();

        //RenderTexture.active = rt;

        //Texture2D tex = new Texture2D(1280, 720, TextureFormat.RGBA32, false);
        //tex.ReadPixels(new Rect(0,0, 1280, 720), 0, 0);
        //RenderTexture.active = null;

    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {

    }
}
