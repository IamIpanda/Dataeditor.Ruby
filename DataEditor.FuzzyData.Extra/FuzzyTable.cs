using System;
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
            value.CopyTo(new_value, 0);
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
    }
}
