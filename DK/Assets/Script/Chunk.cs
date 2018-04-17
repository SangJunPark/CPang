using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : ITickable
{
    public static readonly int CHUNK_WIDTH = 20;
    public static readonly int CHUNK_HEIGHT = 20;

    public readonly int chunkWidth = 20;
    public readonly int chunkHeight = 20;

    private Block[,,] blocks;

    private Vector3 chunkPos;
    private int chunkPx;
    public int ChunkPx { get { return chunkPx; } }
    private int chunkPz;
    public int ChunkPz { get { return chunkPz; } }
    private Vector3 blockSpace = new Vector3(0, 0, 0);

    private GameObject chunkMeshObject;

    private MeshData data;
    protected bool hasGenerated = false;
    protected bool hasRendered = false;
    protected bool hasDrawn = false;
    protected bool needToUpdate = false;

    public bool bVisible;
    MeshRenderer chunkRenderer;
    MeshFilter chunkMesh;


    public Chunk(int px, int pz)
    {
        //chunkPos = pos;
        chunkPx = px;
        chunkPz = pz;
    }

    public Chunk(int px, int pz, int[,,] chunkData)
    {
        //chunkPos = pos;
        chunkPx = px;
        chunkPz = pz;
        blocks = LoadChunkFromData(chunkData);
        hasGenerated = true;
    }

    public virtual  void Start()
    {
        if (hasGenerated)
            return;
        blocks = new Block[chunkWidth, chunkHeight, chunkWidth];
        UpdateBlocks();
        //for(int x = 0; x < chunkWidth; ++ x)
        //{
        //    for (int y = 0; y < chunkHeight; ++y)
        //    {
        //        for (int z = 0; z < chunkWidth; ++z)
        //        {
        //            blocks[x, y, z] = Block.Dirt;
        //        }
        //    }
        //}
        hasGenerated = true;
    }
    public void Initzilize(int chunk_px, int chunk_pz)
    {
        //Set mesh object
        bVisible = true;
        chunkMeshObject = new GameObject();

        Transform t = chunkMeshObject.transform;
        t.gameObject.AddComponent<MeshFilter>();
        t.gameObject.AddComponent<MeshRenderer>();
        t.gameObject.AddComponent<MeshCollider>();

        t.transform.localPosition = new Vector3(chunk_px * chunkWidth, 0, chunk_pz * chunkWidth);

        chunkRenderer = t.GetComponent<MeshRenderer>();

        chunkRenderer.material = Resources.Load("mat_grass") as Material;
        Texture2D tmp = new Texture2D(0, 0);
        tmp.LoadImage(System.IO.File.ReadAllBytes("Assets/Atlas/atlas.png"));
        chunkRenderer.material.mainTexture = tmp;
    }

    public void Tick()
    {
    }


    public virtual void Update()
    {
        if(needToUpdate)
        {
            needToUpdate = false;
            hasDrawn = false;
            hasRendered = false;
        }

        if(!hasDrawn)
        {
            data = new MeshData();
            for (int x = 0; x < chunkWidth; ++x)
            {
                for (int y = 0; y < chunkHeight; ++y)
                {
                    for (int z = 0; z < chunkWidth; ++z)
                    {
                        data.Merge(blocks[x, y, z].Draw(this, blocks, x, y, z, new Vector3(x, y, z) - blockSpace));
                    }
                }   
            }
            hasDrawn = true;
        }

        if(bVisible)
        {
            //if (!chunkRenderer.enabled)
            //    chunkRenderer.enabled = true;
            //else
            //    chunkRenderer.enabled = false;
        }
    }

    public virtual void OnUnityUpdate()
    {
        if((hasGenerated && hasDrawn && !hasRendered))
        {
            Mesh mesh = data.ToMesh();

            chunkMeshObject.GetComponent<MeshFilter>().sharedMesh = mesh;
            chunkMeshObject.GetComponent<MeshCollider>().sharedMesh = mesh;
            hasRendered = true;
        }
    }

    private void UpdateBlocks()
    {
        System.Random rnd = new System.Random();
        for (int x = 0; x < chunkWidth; ++x)
        {
            for (int y = 0; y < chunkHeight; ++y)
            {
                for (int z = 0; z < chunkWidth; ++z)
                {
                    float perlin = GetHeight(x, y, z);
                    if (perlin > GameManager.Scutoff)
                        blocks[x, y, z] = Block.Air;
                    else if (perlin > GameManager.Scutoff * 0.8)
                        blocks[x, y, z] = Block.Beacon;
                    else
                    {
                        blocks[x, y, z] = Block.Dirt;
                        //Logger.Log(" x : " + x + " y : " + y + " z : " + z + " noise :" + perlin);
                    }
                }
            }
        }
    }

    public float GetHeight(float px, float py, float pz)
    {
        px += (chunkPx * chunkWidth);
        pz += (chunkPz * chunkWidth);

        float p1 = Mathf.PerlinNoise(px / GameManager.Sdx, pz / GameManager.Sdz) * GameManager.Smul;
        p1 *= (GameManager.Smy * py);
        return p1;
    }

    internal void SetBlock(int x, int y, int z, Block block)
    {
        blocks[x, y, z] = block;
        needToUpdate = true;
    }

    public void Degenerate()
    {

    }

    public int[,,] GetChunkSaveData()
    {
        return blocks.ToIntArray();
    }

    public Block[,,] LoadChunkFromData(int[,,] blockData)
    {
        return blockData.ToBlockArray();
    }

    //public float GetHeight(float px, float py, float pz)
    //{
    //    px += (chunkPx * chunkWidth);
    //    pz += (chunkPz * chunkWidth);

    //    float r = new System.Random().Next(50, 100);
    //    float p1 = Mathf.PerlinNoise(px / r, pz / r) * GameManager.Smul;
    //    p1 *= (GameManager.Smy * py);
    //    return p1;
    //}
}
