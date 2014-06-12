using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public static class Option
    {
        
        static System.Runtime.Serialization.Formatters.Binary.BinaryFormatter ser = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        static readonly string DIRECTORY = "Program/Options";
        static readonly string EXTENSION = ".option";
        public static object GetOption(string path)
        {
            string full_path = System.IO.Path.Combine(DIRECTORY, path + EXTENSION);
            if (!(System.IO.File.Exists(full_path))) return null;
            System.IO.FileStream stream = null;
            try
            {
                stream = new System.IO.FileStream(full_path, System.IO.FileMode.Open);
                object answer = ser.Deserialize(stream);
                stream.Close();
                return answer;
            }
            catch (Exception ex) { Log.log(ex.ToString()); }
            finally { if (stream != null) { stream.Close(); } }
            return null;
        }
        public static object GetOption(Type type)
        {
            return GetOption(type.ToString());
        }
        public static object GetOption(object ob)
        {
            return GetOption(ob.GetType());
        }
        public static void SetOption(string path,object target)
        {
            string full_path = System.IO.Path.Combine(DIRECTORY, path + EXTENSION);
            try
            {
                System.IO.FileStream stream = new System.IO.FileStream(full_path, System.IO.FileMode.Create);
                ser.Serialize(stream, target);
                stream.Close();
            }
            catch (Exception ex) { Log.log(ex.ToString());} 
        }
        public static void SetOption(Type type,object target)
        {
            SetOption(type.ToString(), target);
        }
        public static void SetOption(object ob, object target)
        {
            SetOption(ob.GetType(), target);
        }
    }
}
