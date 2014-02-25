using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DataEditor.Help
{
    public class Serialization
    {
        static Contract.Serialization Default;
        static List<Contract.Serialization> serializations = new List<Contract.Serialization>();
        static Dictionary<string, Contract.Serialization> flags = new Dictionary<string, Contract.Serialization>();
        static Serialization() { serializations.Add(new DataEditor.FuzzyData.Serialization.RubyMarshal.RubyMarshalAdapter()); LoadDlls(); }
        static void LoadDlls()
        {
            System.IO.DirectoryInfo Directory = new System.IO.DirectoryInfo("Program/Serialization");
            if (!(Directory.Exists)) Directory.Create();
            foreach (Assembly ass in Reflect.GetDirectory(Directory))
                LoadDll(ass);
            Default = serializations[0];
        }
        static void LoadDll(Assembly Ass)
        {
            foreach (Type serialization in Ass.GetExportedTypes())
                if (Fit(serialization))
                {
                    serializations.Add(Ass.CreateInstance(serialization.ToString()) as Contract.Serialization);
                    Log.log("已获得序列化器：" + serialization.FullName);
                }
            foreach (Contract.Serialization serialization in serializations)
                if (serialization is Contract.Iconic)
                {
                    string key = (serialization as Contract.Iconic).Flag;
                    if (flags.ContainsKey(key))
                        Log.log("由于 " + key + " 已存在，下列序列化器被忽略： " + serialization.GetType().ToString());
                    else
                        flags.Add(key, serialization);
                }
        }
        static bool Fit(Type serialization)
        {
            Type ser = typeof(Contract.Serialization);
            foreach (Type t in serialization.GetInterfaces())
                if (t == ser)
                    return true;
            return false;
        }
        static public object LoadFile(string full_name,string ser = "")
        {
            // 选择一个合适的序列化器
            Contract.Serialization serialization = Default;
            if (ser != "")
                if (!flags.TryGetValue(ser, out serialization))
                    throw new ArgumentException("找不到名为 " + ser + " 的序列化器。");
            // 建立文件流
            System.IO.FileStream stream;
            try
            {
                stream = new System.IO.FileStream(full_name, System.IO.FileMode.Open);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An Error Occuued when Loading File " + full_name, ex);
            }
            // 进行序列化
            try
            {
                object ob = serialization.Load(stream);
                stream.Close();
                return ob;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An Error Occured when Serializing File" + full_name, ex);
            }
        }
        static public Contract.Serialization TryGetSerialization(string key)
        {
            Contract.Serialization ser = null;
            flags.TryGetValue(key, out ser);
            if (ser == null) Log.log("序列化器申请被拒绝：" + key);
            return ser;
        }
        static public object TryGetValue(string data, string key)
        {
            Contract.Serialization ser = TryGetSerialization(key);
            if (ser == null) return null;
            var stream = new System.IO.MemoryStream(System.Text.Encoding.Default.GetBytes(data));
            return ser.Load(stream);
        }
    }
}
