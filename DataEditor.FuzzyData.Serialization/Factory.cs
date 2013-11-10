using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData.Serialization
{
    public interface IUserdefinedDumpObject<SerializationType,T>
    {
        SerializationType dump(System.IO.Stream stream, T target, SerializationType Tag);
    }
    public interface IUserdefinedMarshalDumpObject<SerializationType, T>
    {
        T _dump(System.IO.Stream stream, SerializationType type);
    }
    public interface ISerializationFactory<SerializationType> :
        IUserdefinedDumpObject<SerializationType, object>,
        IUserdefinedMarshalDumpObject<SerializationType, object>
    {
        string Type { get; }
        Type Actual { get; }
    }
    public static class Factory<SerializationType>
    {
        static Dictionary<string, ISerializationFactory<SerializationType>> dictionary =
            new Dictionary<string, ISerializationFactory<SerializationType>>();
        static Dictionary<Type, ISerializationFactory<SerializationType>> typeDictionary =
            new Dictionary<Type, ISerializationFactory<SerializationType>>();
        public static void Add(params ISerializationFactory<SerializationType>[] factors)
        {
            foreach (var i in factors)
                if (!dictionary.ContainsKey(i.Type))
                {
                    dictionary.Add(i.Type, i);
                    typeDictionary.Add(i.GetType(), i);
                }
        }
        public static ISerializationFactory<SerializationType> Factor(string str)
        {
                if(dictionary.ContainsKey(str))
                    return dictionary[str];
                else 
                    return null;
        }
        public static ISerializationFactory<SerializationType> Factor(Type type)
        {
            if (typeDictionary.ContainsKey(type))
                return typeDictionary[type];
            else
                return null;
        }
        public static void SearchFactor(System.Reflection.Assembly ass)
        {
            Type Factor = typeof(ISerializationFactory<SerializationType>);
            foreach (Type type in ass.GetExportedTypes())
                    foreach (Type inter in type.GetInterfaces())
                        if (inter == Factor)
                            Add(ass.CreateInstance(type.ToString()) as ISerializationFactory<SerializationType>);
        }
        static string Path = "Program/Serialization/UserDefined";
        static Factory()
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Path);
            foreach (System.IO.FileInfo file in dir.GetFiles("*.dll"))
            {
                System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFile(file.FullName);
                SearchFactor(ass);
            }
        }
    }
}
