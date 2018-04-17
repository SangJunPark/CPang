using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class World : ILoopable {

    private static World _instance = new World();
    public static World Instance { get { return _instance; } }

    private Thread worldThread;
    private bool IsRunning;
    private bool runOnce = false;

    public static Int3 playerPos;
    private static readonly int renderDistanceInChunks = 3;

    public static void Instantiate()
    {
        MainLoopable.GetInstance().RegisterLoop(_instance);
        System.Random r = new System.Random();
        playerPos = new Int3(new Vector3(r.Next(-1000, 1000), 100, r.Next(-1000, 1000)));
    }

    private readonly int worldWidth = 10;
    private readonly int worldHeight = 10;
    private List<Chunk> chunkSet;
    //private Chunk[,] chunkSet;
    public void Start()
    {
        IsRunning = true;
        InitializeChunks();
        Logger.Log("World Start");

        worldThread = new Thread(() =>
        {
            Logger.Log("Initializing WorldThread");
            while (IsRunning)
            {
                try
                {
                    if(!runOnce)
                    {
                        for(int i = 0; i < chunkSet.Count; ++i)
                            chunkSet[i].Start();

                        BlockRegistry.RegisterBlocks();
                        runOnce = true;
                    }

                    for (int i = 0; i < chunkSet.Count; ++i)
                        chunkSet[i].Update();
                }
                catch (System.Exception e)
                {
                    Logger.Log(e);
                }
            }

            Logger.Log("World thread stopped");
            Logger.MainLog.Update();
        });
        worldThread.Start();
    }

    private bool CheckVisible()
    {

        return false;
    }

    public float GetDistance(Vector3 pos)
    {
        Terrain terrain;
           
        return 0f;
    }

    private void InitializeChunks()
    {
        chunkSet = new List<Chunk>();

        for (int x = -renderDistanceInChunks; x < renderDistanceInChunks; ++x)
        {
            for (int y = -renderDistanceInChunks; y < renderDistanceInChunks; ++y)
            {
                Int3 newChunkPos = new Int3(playerPos.x, playerPos.y, playerPos.z);
                newChunkPos.AddPos(new Int3(x, 0, y));
                newChunkPos.ToChunkCoordinates();

                Chunk newChunk = null;
                if (CheckExistChunkData(x, y))
                {
                    try
                    {
                        newChunk = new Chunk(x, y, Serializer.DeSerialize_FromFile<int[,,]>(string.Format("Data/Chunks/C_{0}_{1}.chk", x, y)));
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogError(ex.ToString());
                    }
                }
                else
                    newChunk = new Chunk(x, y);

                newChunk.Initzilize(x, y);
                chunkSet.Add(newChunk);
            }
        }
    }

    public void Update()
    {
        if(chunkSet != null && chunkSet.Count > 0)
        {
            for (int i = 0; i < chunkSet.Count; ++i)
                chunkSet[i].OnUnityUpdate();
        }
    }

    public Chunk GetChunk(int posX, int posZ)
    {
        if (chunkSet != null && chunkSet.Count > 0)
        {
            for (int i = 0; i < chunkSet.Count; ++i)
            {
                if (chunkSet[i].ChunkPx.Equals(posX) && chunkSet[i].ChunkPz.Equals(posZ))
                {
                    return chunkSet[i];
                }
            }
        }
        return new ErroredChunk(0,0);
    }
    public void OnApplicationQuit()
    {
        int iX = 0, iY = 0;
        for(int i = 0; i < chunkSet.Count; ++i)
        {
            iX = i / worldHeight;
            iY = (i % worldWidth);
            Debug.Log(iX + " " + iY);
            Serializer.Serialize_ToFile<int[,,]>(FileManager.Instance.ChunkSaveDirectory, FileManager.Instance.GetChunkString(iX, iY), chunkSet[i].GetChunkSaveData());
        }
        IsRunning = false;
        worldThread.Abort();
    }

    bool CheckExistChunkData(int x, int z)
    {
        return System.IO.File.Exists(FileManager.Instance.GetChunkString(x, z));
    }
}
