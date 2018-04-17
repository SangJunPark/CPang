using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevChunk : Chunk {

	public DevChunk(int px, int pz) : base(px, pz)
    {

    }

    public override void OnUnityUpdate()
    {
        base.OnUnityUpdate();

        //hasGenerated = false;
        hasDrawn = false;
        hasRendered = false;
    }
}
