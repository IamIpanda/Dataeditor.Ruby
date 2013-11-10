using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DataEditor.FuzzyData;

namespace DataEditor.FuzzyData.Serialization.XML
{
    /// <summary>
    /// 将 FuzzyData 结构从 XML 文档序列化或反序列化的序列化类。
    /// 警告：这个类没有完成。此后的一切更改与修正都是可能的，
    /// 因此造成的风险都由使用者自行承担。
    /// </summary>
    public static class XML
    {
        public const byte MarshalMajor = 4;
        public const byte MarshalMinor = 8;
        public const byte XMLMajor = 0;
        public const byte XMLMinor = 6;

        public abstract class IDs
        {
            public static FuzzySymbol encoding = FuzzySymbol.GetSymbol("encoding");
            public static FuzzySymbol E = FuzzySymbol.GetSymbol("E");   
        }

        public abstract class Types
        {
            public const string Nil = "Nil";
            public const string True = "True";
            public const string False = "Flase";
            public const string Fixnum = "Fixnum";
            public const string Extended = "Extended";
            public const string UserClass = "UserClass";
            public const string Object = "Object";
            public const string Data = "Data";
            public const string UserDefined = "UserDefined";
            public const string UserMarshal = "UserMarshal";
            public const string Float = "Float";
            public const string Bignum = "Bignum";
            public const string String = "String";
            public const string Regexp = "Regexp";
            public const string Array = "Array";
            public const string Hash = "Hash";
            public const string HashWithDefault = "HashWithDefault";
            public const string Struct = "Struct";
            public const string ModuleOld = "ModuleOld";
            public const string Class = "Class";
            public const string Module = "Module";
            public const string Symbol = "Symbol";
            public const string SymbolLink = "SymbolLink";
            public const string InstanceVariable = "InstanceVariable";
            public const string Link = "Link";
        }
        public abstract class Options
        {
            /// <summary>
            /// 設置是否忽略符號的編碼。忽略的情形，可以讓生成的XML更加好看一些。
            /// 但是，可能引起一些情況下的問題。
            /// </summary>
            public static bool IgnoreSymbolEncoding = false;
        }
        public static object Load(Stream input)
        {
            XMLReader reader = new XMLReader(input);
            return reader.Load();
        }

        public static void Dump(Stream output, object param)
        {
            XMLWriter writer = new XMLWriter(output);
            writer.Dump(param);
        }
    }
}
