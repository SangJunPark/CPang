using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : ITickable {

    private bool bTransparent;
    public static Block Dirt = new Block(false, "anvil_base");
    public static Block Beacon = new Block(false, "beacon");
    public static Block Air = new Block(true);

    private static int currentId;

    private int blockId;
    public int BlockID { get { return blockId; } }

    private string name;
    public string Name { get { return name; } }

    private Vector2[] uvMap;

    public Block(bool IsTransparent)
    {
        bTransparent = IsTransparent;
        Register();
    }

    public Block(bool IsTransparent, string name)
    {
        bTransparent = IsTransparent;
        this.name = name;
        Register();
    }

    public bool IsTransparent { get { return bTransparent; } }

    private void Register()
    {
        blockId = currentId++;
        BlockRegistry.RegisterBlock(this);
    }

    public void Start()
    {
    }

    public void Update()
    {
    }

    public void OnUnityUpdate()
    {
    }

    public void Tick()
    {
    }

    public virtual MeshData Draw(Chunk chunk, Block[,,] blocks, int x, int y, int z, Vector3 pos)
    {
        if (this.Equals(Air))
            return new MeshData();
        else if (!string.IsNullOrEmpty(name))
        {
            UVMap uvMap = UVMap.GetUVMap(name);
            return MathHelper.DrawCube(chunk, blocks, this, x, y, z, pos, uvMap.m_uvMap);
        }

        return new MeshData();
    }

    public void TickTest()
    {

    }

    //public virtual MeshData Draw(Chunk chunk)
    //{

    //}
}
