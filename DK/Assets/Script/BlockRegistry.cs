using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRegistry  {

    private static readonly bool debugMode = true;
    private static Dictionary<int, Block> registeredBlocks;
    private static readonly string RegistryPath = "BlockRegistry.txt";

    public static void Initialize()
    {
        registeredBlocks = new Dictionary<int, Block>();
        
    }

    private static void Load()
    {
        if(System.IO.File.Exists(RegistryPath))
        {
            
        }
    }

    public static void RegisterBlock(Block block)
    {
        if(block != null)
            registeredBlocks.Add(block.BlockID, block);
    }
    public static void RegisterBlocks()
    {
        if(debugMode)
        {
            int i = 0;
            List<string> names = new List<string>();
            foreach (var blockInfo in registeredBlocks)
            {
                names.Add(string.Format("CurrentID : {0}, BlockName : {1}, BlockID : {2}", i++, blockInfo.Value.Name, blockInfo.Key));
            }
            System.IO.File.WriteAllLines("BlockRegistry.txt", names.ToArray());
        }
    }
	
    internal static Block GetBlockFromID(int blockId)
    {
        try
        {
            if (registeredBlocks.ContainsKey(blockId))
                return registeredBlocks[blockId];
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        return Block.Dirt;
    }
}
