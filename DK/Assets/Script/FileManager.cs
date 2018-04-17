using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager {

    public readonly string ChunkSaveDirectory = "Data/Chunks/";
    private static FileManager instance;

    public static FileManager Instance
    {
        get
        {
            if (instance == null)
                instance = new FileManager();

            return instance;
        }
    }

	public FileManager()
    {

    }

    public void RegisterFiles()
    {
        Serializer.Check_Gen_Folder(ChunkSaveDirectory);
    }

    public string GetChunkString(int x, int z)
    {
        return string.Format("{0}C_{1}_{2}.chk", ChunkSaveDirectory, x, z);
    }
}
