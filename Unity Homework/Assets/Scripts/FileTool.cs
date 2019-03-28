using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class FileTool{
    public static object ReadSaveFile(string path)
    {
        object ret = null;
        if (File.Exists(path))
        {
            FileStream fs = File.Open(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            ret = bf.Deserialize(fs);
            fs.Close();
        }
        return ret;
    }

    public static void WriteSaveFile(string path, object data)
    {
        FileStream fs = File.OpenWrite(path);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, data);
        fs.Close();
    }
}
