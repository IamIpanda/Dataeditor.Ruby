using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DataEditor.FuzzyData.Serialization.RubyMarshal
{
    public class RubyMarshalWriter
    {
        private Stream m_stream;
        private BinaryWriter m_writer;
        private Dictionary<object, int> m_objects;
        private Dictionary<FuzzySymbol, int> m_symbols;
        private Dictionary<object, object> m_compat_tbl;

        public RubyMarshalWriter(Stream output)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }
            if (!output.CanWrite)
            {
                throw new ArgumentException("stream cannot write");
            }
            this.m_stream = output;
            this.m_objects = new Dictionary<object, int>();
            this.m_symbols = new Dictionary<FuzzySymbol, int>();
            this.m_compat_tbl = new Dictionary<object, object>();
            this.m_writer = new BinaryWriter(m_stream);
        }

        /// <summary>
        /// static void w_nbyte(const char *s, long n, struct dump_arg *arg)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public void WriteNByte(byte[] s, int n)
        {
            this.m_writer.Write(s, 0, n);
        }

        /// <summary>
        /// static void w_byte(char c, struct dump_arg *arg)
        /// </summary>
        /// <param name="c"></param>
        public void WriteByte(byte c)
        {
            this.m_writer.Write(c);
        }

        /// <summary>
        /// static void w_long(long x, struct dump_arg *arg)
        /// </summary>
        /// <param name="value"></param>
        public void WriteLong(int value)
        {
            if (value == 0)
            {
                this.m_writer.Write((byte)0);
            }
            else if ((value > 0) && (value < 0x7b))
            {
                this.m_writer.Write((byte)(value + 5));
            }
            else if ((value < 0) && (value > -124))
            {
                this.m_writer.Write((sbyte)(value - 5));
            }
            else
            {
                sbyte num2;
                byte[] buffer = new byte[5];
                buffer[1] = (byte)(value & 0xff);
                buffer[2] = (byte)((value >> 8) & 0xff);
                buffer[3] = (byte)((value >> 0x10) & 0xff);
                buffer[4] = (byte)((value >> 0x18) & 0xff);
                int index = 4;
                if (value >= 0)
                {
                    while (buffer[index] == 0)
                    {
                        index--;
                    }
                    num2 = (sbyte)index;
                }
                else
                {
                    while (buffer[index] == 0xff)
                    {
                        index--;
                    }
                    num2 = (sbyte)-index;
                }
                buffer[0] = (byte)num2;
                this.m_writer.Write(buffer, 0, index + 1);
            }
        }

        /// <summary>
        /// static void w_bytes(const char *s, long n, struct dump_arg *arg)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        public void WriteBytes(byte[] s, int n)
        {
            WriteLong(n);
            WriteNByte(s, n);
        }

        public void WriteBytes(byte[] s)
        {
            WriteBytes(s, s.Length);
        }

        /// <summary>
        /// #define w_cstr(s, arg) w_bytes((s), strlen(s), (arg))
        /// </summary>
        /// <param name="s"></param>
        public void WriteCString(string s)
        {
            WriteLong(s.Length);
            this.m_writer.Write(Encoding.Default.GetBytes(s));
        }

        /// <summary>
        /// static void w_float(double d, struct dump_arg *arg)
        /// </summary>
        /// <param name="value"></param>
        public void WriteFloat(double value)
        {
            if (double.IsInfinity(value))
            {
                if (double.IsPositiveInfinity(value))
                {
                    WriteCString("inf");
                }
                else
                {
                    WriteCString("-inf");
                }
            }
            else if (double.IsNaN(value))
            {
                WriteCString("nan");
            }
            else
            {
                WriteCString(string.Format("{0:g}", value));
            }
        }

        public void WriteFloat(float value)
        {
            WriteFloat((double)value);
        }

        public void WriteFloat(FuzzyFloat value)
        {
            WriteFloat(value.Value);
        }

        /// <summary>
        /// static void w_symbol(ID id, struct dump_arg *arg)
        /// </summary>
        /// <param name="id"></param>
        public void WriteSymbol(FuzzySymbol id)
        {
            string sym;
            int num;
            System.Text.Encoding encidx = null;

            if (this.m_symbols.TryGetValue(id, out num))
            {
                WriteByte(RubyMarshal.Types.SymbolLink);
                WriteLong(num);
            }
            else
            {
                sym = id.Name;
                if (sym.Length == 0)
                {
                    throw new InvalidDataException("can't dump anonymous ID");
                }
                encidx = id.Encoding;
                if (encidx == Encoding.ASCII || encidx == Encoding.Default || encidx == Encoding.UTF8)
                {
                    encidx = null;
                }
                if (encidx != null)
                {
                    WriteByte(RubyMarshal.Types.InstanceVariable);
                }
                WriteByte(RubyMarshal.Types.Symbol);
                WriteCString(sym);

                this.m_symbols.Add(id, this.m_symbols.Count);
                if (encidx != null)
                {
                    WriteEncoding(id, 0);
                }
            }
        }

        /// <summary>
        /// static void w_unique(VALUE s, struct dump_arg *arg)
        /// </summary>
        /// <param name="s"></param>
        public void WriteUnique(FuzzySymbol s)
        {
            WriteSymbol(s);
        }

        /// <summary>
        /// static void w_extended(VALUE klass, struct dump_arg *arg, int check)
        /// </summary>
        /// <param name="klass"></param>
        /// <param name="check"></param>
        public void WriteExtended(object klass, bool check)
        {
            FuzzyObject fobj = klass as FuzzyObject;
            if (fobj != null)
            {
                foreach (FuzzyModule item in fobj.ExtendModules)
                {
                    WriteByte(RubyMarshal.Types.Extended);
                    WriteUnique(item.Symbol);
                }
            }
        }

        /// <summary>
        /// static void w_class(char type, VALUE obj, struct dump_arg *arg, int check)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="check"></param>
        public void WriteClass(byte type, object obj, bool check)
        {
            object real_obj;
            if (this.m_compat_tbl.TryGetValue(obj, out real_obj))
            {
                obj = real_obj;
            }
            FuzzyObject fobj = obj as FuzzyObject;
            if (fobj != null)
            {
                FuzzyClass klass = FuzzyClass.GetClass(fobj.ClassName);
                WriteExtended(klass, check);
                WriteByte(type);
                WriteUnique(fobj.ClassName);
            }
        }

        /// <summary>
        /// static void w_uclass(VALUE obj, VALUE super, struct dump_arg *arg)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="super"></param>
        public void WriteUserClass(object obj, FuzzyClass super)
        {
            FuzzyObject fobj = obj as FuzzyObject;
            if (fobj != null)
            {
                FuzzyClass klass = fobj.Class;
                WriteExtended(klass, true);
                if (klass != super)
                {
                    WriteByte(RubyMarshal.Types.UserClass);
                    WriteUnique(klass.Symbol);
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// static void w_encoding(VALUE obj, long num, struct dump_call_arg *arg)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="num"></param>
        public void WriteEncoding(object obj, int num)
        {
            Encoding encidx = null;
            if (obj is FuzzyObject)
                encidx = (obj as FuzzyObject).Encoding;

            if (encidx == null)
            {
                WriteLong(num);
                return;
            }
            WriteLong(num + 1);

            if (encidx == Encoding.Default)
            {
                /* special treatment for US-ASCII and UTF-8 */
                WriteSymbol(RubyMarshal.IDs.E);
                WriteObject(false);
                return;
            }
            else if (encidx == Encoding.UTF8)
            {
                WriteSymbol(RubyMarshal.IDs.E);
                WriteObject(true);
                return;
            }

            WriteSymbol(RubyMarshal.IDs.encoding);
            WriteObject(FuzzySymbol.GetSymbol(encidx.BodyName));

        }

        /// <summary>
        /// static void w_ivar(VALUE obj, st_table *tbl, struct dump_call_arg *arg)
        /// </summary>
        /// <param name="obj"></param>
        public void WriteInstanceVariable(FuzzyObject obj, Dictionary<FuzzySymbol, object> tbl)
        {
            int num = tbl != null ? tbl.Count : 0;

            WriteEncoding(obj, num);
            if (tbl != null)
            {
                foreach (KeyValuePair<FuzzySymbol, object> item in tbl)
                {
                    if (item.Key == RubyMarshal.IDs.encoding) continue;
                    if (item.Key == RubyMarshal.IDs.E) continue;
                    WriteSymbol(item.Key);
                    WriteObject(item.Value);
                }
            }
        }

        /// <summary>
        /// static void w_objivar(VALUE obj, struct dump_call_arg *arg)
        /// </summary>
        /// <param name="obj"></param>
        public void WriteObjectInstanceVariable(FuzzyObject obj)
        {
            WriteInstanceVariable(obj, obj.InstanceVariables);
        }

        /// <summary>
        /// static void w_object(VALUE obj, struct dump_arg *arg, int limit)
        /// </summary>
        /// <param name="obj"></param>
        public void WriteObject(object obj)
        {
            int num;
            if (this.m_objects.TryGetValue(obj, out num))
            {
                WriteByte(RubyMarshal.Types.Link);
                WriteLong(num);
                return;
            }
            if (obj == null || obj == FuzzyNil.Instance)
            {
                WriteByte(RubyMarshal.Types.Nil);
            }
            else if (obj is bool && (bool)obj == true)
                WriteByte(RubyMarshal.Types.True);
            else if (obj is bool && (bool)obj == false)
                WriteByte(RubyMarshal.Types.False);
            else if (obj is FuzzyBool && (obj as FuzzyBool).Value)
                WriteByte(RubyMarshal.Types.True);
            else if (obj is FuzzyBool && !(obj as FuzzyBool).Value)
                WriteByte(RubyMarshal.Types.False);
            else if (obj is int || obj is long || obj is FuzzyFixnum)
            {
                long v;
                if (obj is int | obj is long)
                    v = (long)obj;
                else
                    v = (obj as FuzzyFixnum).Value;
                // (2**30).class   => Bignum
                // (2**30-1).class => Fixnum
                // (-2**30-1).class=> Bignum
                // (-2**30).class  => Fixnum
                if (v <= 1073741823 && v >= -1073741824)
                {
                    WriteByte(RubyMarshal.Types.Fixnum);
                    WriteLong((int)v);
                }
                else
                {
                    WriteObject(FuzzyBignum.Create(v));
                }
            }
            else if (obj is FuzzySymbol)
            {
                WriteSymbol(obj as FuzzySymbol);
            }
            else
            {
                FuzzyObject fobj = obj as FuzzyObject;
                bool hasiv = false;
                if (fobj != null)
                    hasiv = (obj as FuzzyObject).InstanceVariables.Count > 0 || fobj.Encoding != null;
                var factor = Serialization.Factory<byte[]>.Factor(fobj.GetType());
                if (factor != null)
                {
                    WriteSymbol(fobj.ClassName);
                    factor.dump(m_stream, fobj, null);
                }
                if (obj is IRubyUserdefinedMarshalDumpObject)
                {
                    this.m_objects.Add(obj, this.m_objects.Count);
                    object result = (obj as IRubyUserdefinedMarshalDumpObject).Dump();
                    if (hasiv)
                        WriteByte(RubyMarshal.Types.InstanceVariable);
                    WriteClass(RubyMarshal.Types.UserMarshal, obj, false);
                    WriteObject(result);
                    if (hasiv)
                        WriteObjectInstanceVariable(fobj);
                    return;
                }
                if (obj is IRubyUserdefinedDumpObject)
                {
                    byte[] result = (obj as IRubyUserdefinedDumpObject).Dump();
                    if (hasiv)
                        WriteByte(RubyMarshal.Types.InstanceVariable);
                    WriteClass(RubyMarshal.Types.UserDefined, obj, false);
                    WriteBytes(result, result.Length);
                    if (hasiv)
                        WriteObjectInstanceVariable(fobj);
                    this.m_objects.Add(obj, this.m_objects.Count);
                    return;
                }
                
                this.m_objects.Add(obj, this.m_objects.Count);


                if (hasiv)
                    WriteByte(RubyMarshal.Types.InstanceVariable);

                if (obj is FuzzyClass)
                {
                    WriteByte(RubyMarshal.Types.Class);
                    WriteCString((obj as FuzzyClass).Name);
                }
                else if (obj is FuzzyModule)
                {
                    WriteByte(RubyMarshal.Types.Module);
                    WriteCString((obj as FuzzyModule).Name);
                }
                else if (obj is float)
                {
                    WriteByte(RubyMarshal.Types.Float);
                    WriteFloat((float)obj);
                }
                else if (obj is double)
                {
                    WriteByte(RubyMarshal.Types.Float);
                    WriteFloat((double)obj);
                }
                else if (obj is FuzzyFloat)
                {
                    WriteByte(RubyMarshal.Types.Float);
                    WriteFloat((FuzzyFloat)obj);
                }
                else if (obj is FuzzyBignum || obj is FuzzyBignumAdapter)
                {
                    FuzzyBignum value;
                    if (obj is FuzzyBignumAdapter)
                        value = (obj as FuzzyBignumAdapter).Value;
                    else
                        value = (FuzzyBignum)obj;
                    char ch;
                    if (value.Sign > 0)
                        ch = '+';
                    else if (value.Sign < 0)
                        ch = '-';
                    else
                        ch = '0';
                    this.m_writer.Write((byte)ch);
                    uint[] words = value.GetWords();
                    int num2 = words.Length * 2;
                    int index = words.Length - 1;
                    bool flag = false;
                    if ((words.Length > 0) && ((words[index] >> 0x10) == 0))
                    {
                        num--;
                        flag = true;
                    }
                    this.WriteLong(num2);
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (flag && (i == index))
                        {
                            this.m_writer.Write((ushort)words[i]);
                        }
                        else
                        {
                            this.m_writer.Write(words[i]);
                        }
                    }
                }
                else if (obj is FuzzyString || obj is string)
                {
                    FuzzyString v;
                    if (obj is string)
                        v = new FuzzyString(obj as string);
                    else
                        v = (FuzzyString)obj;
                    WriteUserClass(v, FuzzyClass.GetClass("String"));
                    WriteByte(RubyMarshal.Types.String);
                    WriteBytes(v.Raw);
                }
                else if (obj is FuzzyRegexp)
                {
                    FuzzyRegexp v = (FuzzyRegexp)obj;
                    WriteUserClass(obj, FuzzyClass.GetClass("Regexp"));
                    WriteByte(RubyMarshal.Types.Regexp);
                    WriteBytes(v.Pattern.Raw);
                    WriteByte((byte)v.Options);
                }
                else if (obj is FuzzyArray || obj is List<object>)
                {
                    FuzzyArray v;
                    if (obj is List<object>)
                        v = new FuzzyArray(obj as List<object>);
                    else
                        v = (FuzzyArray)obj;
                    WriteUserClass(v, FuzzyClass.GetClass("Array"));
                    WriteByte(RubyMarshal.Types.Array);
                    WriteLong(v.Length);
                    for (int i = 0; i < v.Count; i++)
                        WriteObject(v[i]);
                }
                else if (obj is FuzzyHash)
                {
                    FuzzyHash v = (FuzzyHash)obj;
                    WriteUserClass(obj, FuzzyClass.GetClass("Hash"));
                    WriteByte(v.DefaultValue != null ? RubyMarshal.Types.HashWithDefault : RubyMarshal.Types.Hash);
                    WriteLong(v.Length);
                    foreach (KeyValuePair<object, object> item in v)
                    {
                        WriteObject(item.Key);
                        WriteObject(item.Value);
                    }
                    if (v.DefaultValue != null) WriteObject(v.DefaultValue);
                }
                else if (obj is FuzzyStruct)
                {
                    FuzzyStruct v = (FuzzyStruct)obj;
                    WriteUserClass(obj, FuzzyClass.GetClass("Struct"));
                    WriteLong(v.InstanceVariables.Count);
                    foreach (KeyValuePair<FuzzySymbol, object> item in v.InstanceVariables)
                    {
                        WriteObject(item.Key);
                        WriteObject(item.Value);
                    }
                }
                else if (obj is FuzzyObject)
                {
                    WriteClass(RubyMarshal.Types.Object, obj, true);
                    WriteObjectInstanceVariable((FuzzyObject)obj);
                }
                else
                {
                    throw new InvalidDataException(string.Format("can't dump {0}", obj.GetType().FullName));
                }
                if (hasiv)
                    WriteInstanceVariable(fobj, fobj.InstanceVariables);
            }
        }

        public void Dump(object obj)
        {
            this.m_writer.Write((byte)4);
            this.m_writer.Write((byte)8);
            WriteObject(obj);
            this.m_stream.Flush();
        }
    }
}