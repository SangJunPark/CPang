using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int3
{
    public int x, y, z;
    public Int3(int _x, int _y, int _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    public Int3(Vector3 pos)
    {
        x = (int)pos.x;
        y = (int)pos.y;
        z = (int)pos.z;
    }

    public override string ToString()
    {
        return string.Format("");
    }

    internal void AddPos(Int3 int3)
    {
        x = int3.x;
        y = int3.y;
        z = int3.z;
    }

    internal void ToChunkCoordinates()
    {
        x = Mathf.FloorToInt(x / Chunk.CHUNK_WIDTH);
        z = Mathf.FloorToInt(z / Chunk.CHUNK_WIDTH);
    }
}

public static class MathHelper {

	public static MeshData DrawCube(Chunk chunk, Block[,,] blocks, Block block, int x, int y, int z, Vector3 pos, Vector2[] uvMap)
    {
        MeshData meshData = new MeshData();

        if(block.Equals(Block.Air))
        {
            return new MeshData();
        }
        if (y - 1 <= 0 || blocks[x, y - 1, z].IsTransparent)
        {
            meshData.Merge(new MeshData(
                new List<Vector3>()
                {
                    new Vector3(0,0,0),
                    new Vector3(0,0,1),
                    new Vector3(1,0,0),
                    new Vector3(1,0,1),
                },
                new List<int>() { 0, 2, 1, 3, 1, 2 },
                new List<Vector2>(uvMap)
            ));
        }

        if (y + 1 >= chunk.chunkHeight || blocks[x, y + 1, z].IsTransparent)
        {
            meshData.Merge(new MeshData(
                new List<Vector3>()
                {
                    new Vector3(0,1,0),
                    new Vector3(0,1,1),
                    new Vector3(1,1,0),
                    new Vector3(1,1,1),
                },
                new List<int>() { 0, 1, 2, 3, 2, 1 },
                new List<Vector2>(uvMap)
            ));
        }

        if (x + 1 >= chunk.chunkWidth || blocks[x + 1, y, z].IsTransparent)
        {
            meshData.Merge(new MeshData(
                new List<Vector3>()
                {
                    new Vector3(1,0,0),
                    new Vector3(1,0,1),
                    new Vector3(1,1,0),
                    new Vector3(1,1,1),
                },
                new List<int>() { 0, 2, 1, 3, 1, 2 },
                new List<Vector2>(uvMap)
            ));
        }

        if (x - 1 <= 0 || blocks[x - 1, y, z].IsTransparent)
        {
            meshData.Merge(new MeshData(
                new List<Vector3>()
                {
                    new Vector3(0,0,0),
                    new Vector3(0,0,1),
                    new Vector3(0,1,0),
                    new Vector3(0,1,1),
                },
                new List<int>() { 0, 1, 2, 3, 2, 1 },
                new List<Vector2>(uvMap)
            ));
        }

        if (z + 1 >= chunk.chunkWidth || blocks[x, y, z + 1].IsTransparent)
        {
            meshData.Merge(new MeshData(
                new List<Vector3>()
                {
                    new Vector3(0,0,1),
                    new Vector3(1,0,1),
                    new Vector3(0,1,1),
                    new Vector3(1,1,1),
                },
                new List<int>() { 0, 1, 2, 3, 2, 1 },
                new List<Vector2>(uvMap)
            ));
        }

        if (z - 1 <= 0 || blocks[x, y, z - 1].IsTransparent)
        {
            meshData.Merge(new MeshData(
                new List<Vector3>()
                {
                    new Vector3(0,0,0),
                    new Vector3(1,0,0),
                    new Vector3(0,1,0),
                    new Vector3(1,1,0),
                },
                new List<int>() { 0, 2, 1, 3, 1, 2 },
               new List<Vector2>(uvMap)
            ));
        }

        meshData.AddPos(pos);

        //Debug.Log(meshData);
        return meshData;
    }

    internal static void AddBlock(Vector3 roundedPosition, Block cabbleStone)
    {
        if (roundedPosition.y > Chunk.CHUNK_HEIGHT)
            return;
        int chunkPosX = Mathf.FloorToInt(roundedPosition.x / Chunk.CHUNK_WIDTH);
        int chunkPosZ = Mathf.FloorToInt(roundedPosition.z / Chunk.CHUNK_WIDTH);

        Chunk currentChunk;
        int xx = 0, yy = 0, zz = 0;
        try
        {
            currentChunk = World.Instance.GetChunk(chunkPosX, chunkPosZ);
            if (currentChunk == null || currentChunk.GetType().Equals(typeof(ErroredChunk)))
            {
                return;
            }
            int x = (int)(roundedPosition.x - (chunkPosX * Chunk.CHUNK_WIDTH));
            int z = (int)(roundedPosition.z - (chunkPosZ * Chunk.CHUNK_WIDTH));
            int y = (int)(roundedPosition.y);
            xx = x;
            yy = y;
            zz = z;
            currentChunk.SetBlock(x-1, y-1, z-1, cabbleStone);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString() + xx + " " + yy + " " + zz );
        }
    }
}
