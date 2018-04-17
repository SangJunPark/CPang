using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtension {

    public static int[,,] ToIntArray(this Block[,,] chunkData)
    {
        int lx = chunkData.GetLength(0);
        int ly = chunkData.GetLength(1);
        int lz = chunkData.GetLength(2);
        int[,,] data = new int[lx, ly, lz];
        for(int x = 0; x < lx; ++x)
        {
            for (int y = 0; y < ly; ++y)
            {
                for (int z = 0; z < lz; ++z)
                {
                    data[x, y, z] = chunkData[x, y, z].BlockID;
                }
            }
        }
        return data;
    }

    public static Block[,,] ToBlockArray(this int[,,] chunkData)
    {
        int lx = chunkData.GetLength(0);
        int ly = chunkData.GetLength(1);
        int lz = chunkData.GetLength(2);
        Block[,,] data = new Block[lx, ly, lz];
        for (int x = 0; x < lx; ++x)
        {
            for (int y = 0; y < ly; ++y)
            {
                for (int z = 0; z < lz; ++z)
                {
                    data[x, y, z] = BlockRegistry.GetBlockFromID(chunkData[x, y, z]);
                }
            }
        }
        return data;
    }
}
