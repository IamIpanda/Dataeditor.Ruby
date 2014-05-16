﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    public partial class FuzzyTable : FuzzyObject
    {
        short[, ,] value;
        int x_size, y_size, z_size, dimension;
        public FuzzyTable(int x_size, int y_size, int z_size)
        {
            value = new short[x_size, y_size, z_size];
            this.x_size = x_size;
            this.y_size = y_size;
            this.z_size = z_size;
            this.dimension = 3;
            this.ClassName = FuzzySymbol.GetSymbol("Table");
        }
        public FuzzyTable(int x_size, int y_size)
            : this(x_size, y_size, 1)
        {
            this.dimension = 2;
        }
        public FuzzyTable(int x_size)
            : this(x_size, 1, 1)
        
        {
            this.dimension = 1;
        }
        public short this[int x]
        {
            get { return this[x, 0, 0]; }
            set { this[x, 0, 0] = value; }
        }
        public short this[int x, int y]
        {
            get { return this[x, y, 0]; }
            set { this[x, y, 0] = value; }
        }
        public short this[int x, int y, int z]
        {
            get { return GetValue(x, y, z); }
            set { SetValue(x, y, z, value); }
        }
        public int xsize { get { return x_size; } }
        public int ysize { get { return y_size; } }
        public int zsize { get { return z_size; } }
        public void resize(int x_size, int y_size, int z_size)
        {
            short[, ,] new_value = new short[x_size, y_size, z_size];
            for (int i = 0; i < x_size; i++)
                for (int j = 0; j < y_size; j++)
                    for (int k = 0; k < z_size; k++)
                        new_value[i, j, k] = SafeGetValue(i, j, k);
            // value.CopyTo(new_value, 0);
            value = new_value;
            this.x_size = x_size;
            this.y_size = y_size;
            this.z_size = z_size;
            this.dimension = 3;
        }
        public void resize(int x_size, int y_size)
        {
            resize(x_size, y_size, 1);
            this.dimension = 2;
        }
        public void resize(int x_size)
        {
            resize(x_size, 1, 1);
            this.dimension = 1;
        }
        protected short SafeGetValue(int x, int y, int z)
        {
            if (x < x_size && y < y_size && z < z_size)
                return value[x, y, z];
            return 0;
        }
        protected short GetValue(int x, int y, int z)
        {
            if (x < x_size && y < y_size && z < z_size)
                return value[x, y, z];
            throw new ArgumentException("Index out of border : " + x.ToString() + "," + y.ToString() + "," + z.ToString());
        }
        protected void SetValue(int x, int y, int z, short value)
        {
            if (x < x_size && y < y_size && z < z_size)
                this.value[x, y, z] = value;
            else
              throw new ArgumentException("Index out of border : " + x.ToString() + "," + y.ToString() + "," + z.ToString());
        }
        /// <summary>
        /// 核心超频连线！
        /// 注意！这个操作符变更了它的原本含义。
        /// 将左操作数变成右操作数的别称。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static FuzzyTable operator |(FuzzyTable self, FuzzyTable source)
        {
            self.x_size = source.x_size;
            self.y_size = source.y_size;
            self.z_size = source.z_size;
            self.dimension = source.dimension;
            self.value = source.value;
            return self;
        }
    }
}
