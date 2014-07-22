﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("FuzzyArray: Count = {Count}")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(FuzzyArrayDebugView))]
    public partial class FuzzyArray : FuzzyObject, IEnumerable<object>, System.Collections.IEnumerable,ICloneable
    {
        public List<object> list;

        public object this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public static implicit operator List<object>(FuzzyArray that)
        {
            return that.list;
        }

        public static implicit operator FuzzyArray(List<object> that)
        {
            return new FuzzyArray(that);
        }

        public FuzzyArray()
        {
            list = new List<object>();
            this.ClassName = FuzzySymbol.GetSymbol("Array");
        }
        public FuzzyArray(IEnumerable<object> collection)
        {
            list = new List<object>(collection);
            this.ClassName = FuzzySymbol.GetSymbol("Array");
        }
        public FuzzyArray(int capacity)
        {
            list = new List<object>(capacity);
            this.ClassName = FuzzySymbol.GetSymbol("Array");
        }
        public int Capacity { get { return list.Capacity; } set { list.Capacity = value; } }
        public int Count { get { return list.Count; } }
        public int Size { get { return list.Count; } }
        public int Length { get { return list.Count; } }
        public void Add(object item) { list.Add(item); }
        public void AddRange(IEnumerable<object> collection) { list.AddRange(collection); }
        public System.Collections.ObjectModel.ReadOnlyCollection<object> AsReadOnly() { return list.AsReadOnly(); }
        public int BinarySearch(object item) { return list.BinarySearch(item); }
        public int BinarySearch(object item, IComparer<object> comparer) { return list.BinarySearch(item, comparer); }
        public int BinarySearch(int index, int count, object item, IComparer<object> comparer) { return list.BinarySearch(index, count, item, comparer); }
        public void Clear() { list.Clear(); }
        public bool Contains(object item) { return list.Contains(item); }
        public List<TOutput> ConvertAll<TOutput>(Converter<object, TOutput> converter) { return list.ConvertAll<TOutput>(converter); }
        public void CopyTo(object[] array) { list.CopyTo(array); }
        public void CopyTo(object[] array, int arrayIndex) { list.CopyTo(array, arrayIndex); }
        public void CopyTo(int index, object[] array, int arrayIndex, int count) { list.CopyTo(index, array, arrayIndex, count); }
        public bool Exists(Predicate<object> match) { return list.Exists(match); }
        public object Find(Predicate<object> match) { return list.Find(match); }
        public List<object> FindAll(Predicate<object> match) { return list.FindAll(match); }
        public int FindIndex(Predicate<object> match) { return list.FindIndex(match); }
        public int FindIndex(int startIndex, Predicate<object> match) { return list.FindIndex(startIndex, match); }
        public int FindIndex(int startIndex, int count, Predicate<object> match) { return list.FindIndex(startIndex, count, match); }
        public object FindLast(Predicate<object> match) { return list.FindLast(match); }
        public int FindLastIndex(Predicate<object> match) { return list.FindLastIndex(match); }
        public int FindLastIndex(int startIndex, Predicate<object> match) { return list.FindLastIndex(startIndex, match); }
        public int FindLastIndex(int startIndex, int count, Predicate<object> match) { return list.FindLastIndex(startIndex, count, match); }
        public void ForEach(Action<object> action) { list.ForEach(action); }
        public List<object>.Enumerator GetEnumerator() { return list.GetEnumerator(); }
        public List<object> GetRange(int index, int count) { return list.GetRange(index, count); }
        public int IndexOf(object item) { return list.IndexOf(item); }
        public int IndexOf(object item, int index) { return list.IndexOf(item, index); }
        public int IndexOf(object item, int index, int count) { return list.IndexOf(item, index); }
        public void Insert(int index, object item) { list.Insert(index, item); }
        public void InsertRange(int index, IEnumerable<object> collection) { list.InsertRange(index, collection); }
        public int LastIndexOf(object item) { return list.LastIndexOf(item); }
        public int LastIndexOf(object item, int index) { return list.LastIndexOf(item, index); }
        public int LastIndexOf(object item, int index, int count) { return list.LastIndexOf(item, index, count); }
        public bool Remove(object item) { return list.Remove(item); }
        public int RemoveAll(Predicate<object> match) { return list.RemoveAll(match); }
        public void RemoveAt(int index) { list.RemoveAt(index); }
        public void RemoveRange(int index, int count) { list.RemoveRange(index, count); }
        public void Reverse() { list.Reverse(); }
        public void Reverse(int index, int count) { list.Reverse(index, count); }
        public void Sort() { list.Sort(); }
        public void Sort(Comparison<object> comparison) { list.Sort(comparison); }
        public void Sort(IComparer<object> comparer) { list.Sort(comparer); }
        public void Sort(int index, int count, IComparer<object> comparer) { list.Sort(index, count, comparer); }
        public object[] ToArray() { return list.ToArray(); }
        public void TrimExcess() { list.TrimExcess(); }
        public bool TrueForAll(Predicate<object> match) { return list.TrueForAll(match); }

        public List<TOutput> Map<TOutput>(Converter<object, TOutput> converter) { return list.ConvertAll<TOutput>(converter); }
        public List<TOutput> Collect<TOutput>(Converter<object, TOutput> converter) { return list.ConvertAll<TOutput>(converter); }
        public void Each(Action<object> action) { Array.ForEach<object>(this.ToArray(), action); }
        public void Push(object item) { 
            list.Add(item); 
        }
        public void Push(params object[] items)
        {
            list.AddRange(items);
        }
        public object Pop() {
            object obj = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return obj;
        }
        public object Shift()
        {
            object obj = list[0];
            list.RemoveAt(0);
            return obj;
        }
        public void Unshift(object item)
        {
            list.Insert(0, item);
        }
        public void Unshift(params object[] items)
        {
            list.InsertRange(0, items);
        }

        internal class FuzzyArrayDebugView
        {
            private FuzzyArray hashtable;
            public FuzzyArrayDebugView(FuzzyArray hashtable)
            {
                this.hashtable = hashtable;
            }

            [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.RootHidden)]
            public object[] Objects
            {
                get
                {
                    return this.hashtable.ToArray();
                }
            }
        }
        /// <summary>
        /// 返回此结构体的拷贝。
        /// 注意！这一拷贝是一个尽可能完全的拷贝。
        /// </summary>
        /// <returns>此结构体的深层拷贝</returns>
        public override  object Clone()
        {
            FuzzyArray fa = new FuzzyArray();
            foreach (object ob in this)
                if (ob is ICloneable)
                    fa.Add((ob as ICloneable).Clone());
                else
                    fa.Add(ob);
            return fa;
        }
        public override void Clone(FuzzyObject source)
        {
            base.Clone(source);
            FuzzyArray S = source as FuzzyArray;
            if (S != null)
            {
                Clear();
                foreach (object ob in S)
                    if (ob is ICloneable)
                        Add((ob as ICloneable).Clone());
                    else
                        Add(ob);
            }
        }
        /// <summary>
        /// 核心超频连线！
        /// 注意！这个操作符变更了它的原本含义。
        /// 将左操作数变成右操作数的别称。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static FuzzyArray operator | (FuzzyArray self, FuzzyArray source)
        {
            self.list = source.list;
            return self;
        }
    }
}
