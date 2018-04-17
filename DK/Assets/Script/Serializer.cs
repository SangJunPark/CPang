using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer {

    public static bool Check_Gen_Folder(string path)
    {
        if(Directory.Exists(path))
        {
            return true;
        }
        else
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        return false;
    }
    public static void Serialize_ToFile<T>(string path, string fileName, string extension, T data) where T: class
    {
        if(Check_Gen_Folder(path))
        {
            try
            {
                using (Stream stream = File.OpenWrite(string.Format("{0}{1}.{2}", path, fileName, extension)))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, data);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }
    }

    public static void Serialize_ToFile<T>(string dirPath, string fullPath, T data) where T : class
    {
        if (Check_Gen_Folder(dirPath))
        {
            try
            {
                using (Stream stream = File.OpenWrite(fullPath))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, data);
                }
            }
            catch (System.Exception ex)
            {

            }
        }
    }

    public static T DeSerialize_FromFile<T>(string path) where T : class
    {
        if(File.Exists(path))
        {
            using (Stream stream = File.OpenRead(path))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return bf.Deserialize(stream) as T;
                }
                catch (System.Exception ex)
                {
                	
                }
            }
        }
        return null;
    }
}
