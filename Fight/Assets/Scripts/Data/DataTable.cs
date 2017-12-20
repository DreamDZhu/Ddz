using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

using CSVTable = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;
using CacheTable = System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.Dictionary<int, object>>;

/// <summary>
/// 读取DataTable目录下的CSV文件
/// </summary>
public class DataTable  {

    static CacheTable cache = new CacheTable();

    /// <summary>
    /// 获取指定ID的那条CSV属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public static T Get<T>(int id) {

        cache = new CacheTable();

        //获得要生成的那个类
        var type = typeof(T);
        //判断缓存中是否包含该类
        if (!cache.ContainsKey(type)) {
            cache[type] = Load<T>("DataTable/" + type.Name);
        }

        T data = (T)cache[type][id];

        return data;
    }

    /// <summary>
    /// 根据路径读取CSV文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    private static Dictionary<int, object> Load<T>(string path) {

        //根据ID读取CSV
        Dictionary<int, object> datas = new Dictionary<int, object>();
        var textAsset = Resources.Load<TextAsset>(path);
        var table = Parser(textAsset.text);

        FieldInfo[] fields = typeof(T).GetFields();
        foreach (var id in table.Keys) {
            var row = table[id];
            var obj = Activator.CreateInstance<T>();
            foreach (FieldInfo fi in fields) {
                fi.SetValue(obj, Convert.ChangeType(row[fi.Name], fi.FieldType));
            }
            datas[Convert.ToInt32(id)] = obj;
        }

        return datas;
    }

    /// <summary>
    /// 根据文本内容解析CSV
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static CSVTable Parser(string content) {


        CSVTable result = new CSVTable();
        //分割每一行
        string[] row = content.Replace("\r", "").Split(new char[] { '\n' });
        //头行
        string[] columnHeads = row[0].Split(new char[] { ',' });

        for (int i = 1; i < row.Length; i++) {
            string[] line = row[i].Split(new char[] { ',' });
            var id = line[0];
            if (String.IsNullOrEmpty(id)) break;

            result[id] = new Dictionary<string, string>();

            for(int j = 0; j < line.Length; j++) {
                result[id][columnHeads[j]] = line[j];
            }
        }

        return result;
    }
}
