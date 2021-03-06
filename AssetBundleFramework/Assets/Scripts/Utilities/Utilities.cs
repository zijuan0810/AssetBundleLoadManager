﻿/*
 * Description:             工具静态类
 * Author:                  tanghuan
 * Create Date:             2018/02/26
 */

using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 工具静态类
/// </summary>
public static class Utilities
{
    /// <summary>
    /// 本机打开特定目录(暂时只用于Windows)
    /// </summary>
    /// <param name="folderPath"></param>
    public static void OpenFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(folderPath, "explorer.exe");
            Process.Start(startInfo);
        }
        else
        {
            UnityEngine.Debug.LogError(string.Format("{0} Directory does not exist!", folderPath));
        }
    }

    /// <summary>
    /// 检查指定目录是否存在，不存在创建一个
    /// </summary>
    public static void checkAndCreateSpecificFolder(string folderpath)
    {
        if (!Directory.Exists(folderpath))
        {
            Directory.CreateDirectory(folderpath);
        }
    }

    /// <summary>
    /// 无论目录是否存在都删除所有文件重新创建一个目录
    /// </summary>
    public static void recreateSpecificFolder(string folderpath)
    {
        if (Directory.Exists(folderpath))
        {
            Directory.Delete(folderpath, true);
        }
        Directory.CreateDirectory(folderpath);
    }

    /// <summary>
    /// 获取文件的目录名字
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    public static string getFileFolderName(string filepath)
    {
        return Path.GetFileName(Path.GetDirectoryName(filepath));
    }

    /// <summary>
    /// 序列化数据到指定文件
    /// </summary>
    /// <param name="filefullpath"></param>
    /// <param name="obj"></param>
    public static void serializeDataToFile(string filefullpath, object obj)
    {
        var bf = new BinaryFormatter();
        var s = new FileStream(filefullpath, FileMode.CreateNew, FileAccess.Write);
        bf.Serialize(s, obj);
        s.Close();
    }

    /// <summary>
    /// 反序列化数据到指定对象
    /// </summary>
    /// <param name="filefullpath"></param>
    /// <returns></returns>
    public static System.Object deserializeDataFromFile(string filefullpath)
    {
        var bf = new BinaryFormatter();
        TextAsset text = Resources.Load<TextAsset>(filefullpath);
        Stream s = new MemoryStream(text.bytes);
        System.Object obj = bf.Deserialize(s);
        s.Close();
        return obj;
    }
}
