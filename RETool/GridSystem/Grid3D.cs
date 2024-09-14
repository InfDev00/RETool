using UnityEngine;

namespace RETool.GridSystem
{
    public class Grid3D<T>
    {
        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }

        public float UnitGridSize = 1;
        public Vector3 Gap = new Vector3(0, 0, 0);

        protected readonly T[,,] _gridArray;

        #region Constructor

        public Grid3D(int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
            
            _gridArray = new T[depth, height, width];
        }

        public Grid3D(int size)
        {
            Width = size;
            Height = size;
            Depth = size;
            
            _gridArray = new T[size, size, size];
        }

        #endregion
        
        #region Coordinate

        public void SetValue(int x, int y, int z, T value, bool warning = false)
        {
            if (IsValidCoordinate(x, y, z)) _gridArray[z, y, x] = value;
            else if (warning) throw new IndexOutOfRangeException("Given coordinate is out of range");
        }

        public T? GetValue(int x, int y, int z, bool warning = false)
        {
            if (IsValidCoordinate(x, y, z)) return _gridArray[z, y, x];
            if (warning) throw new IndexOutOfRangeException("Given coordinate is out of range");

            return default;
        }
        
        public bool IsValidCoordinate(int x, int y, int z) => x >= 0 && x < Width && y >= 0 && y < Height && z >= 0 && z < Depth;

        public void ForEach(Action<int, int, int, T> action)
        {
            for (var z = 0; z < Depth; z++)
            {
                for (var y = 0; y < Height; y++)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        action(x, y, z, _gridArray[z, y, x]);
                    }
                }
            }
        }
        
        #endregion

        #region Vector

        public Vector3 GetLocalPosition(int x, int y, int z)
        {
            //Get the Center position of each grid
            return new Vector3(
                x * (UnitGridSize + Gap.x) + UnitGridSize / 2,
                y * (UnitGridSize + Gap.y) + UnitGridSize / 2,
                z * (UnitGridSize + Gap.z) + UnitGridSize / 2
            );
        }

        public void GetXYZ(Vector3 position, out int x, out int y, out int z)
        {
            x = Mathf.FloorToInt((position.x - UnitGridSize / 2) / (UnitGridSize + Gap.x));
            y = Mathf.FloorToInt((position.y - UnitGridSize / 2) / (UnitGridSize + Gap.y));
            z = Mathf.FloorToInt((position.z - UnitGridSize / 2) / (UnitGridSize + Gap.z));

            if (!IsValidCoordinate(x, y, z)) throw new IndexOutOfRangeException("Given Vector3 is out of range");
        }

        public virtual void SetValue(Vector3 position, T value, bool warning = false)
        {
            GetXYZ(position, out var x, out var y, out var z);
            SetValue(x, y, z, value, warning);
        }

        public virtual T? GetValue(Vector3 position, bool warning = false)
        {
            GetXYZ(position, out var x, out var y, out var z);
            return GetValue(x, y, z, warning);
        }

        public virtual void ForEach(Action<Vector3, T> action)
        {
            for (var z = 0; z < Depth; z++)
            {
                for (var y = 0; y < Height; y++)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        action(GetLocalPosition(x, y, z), _gridArray[z, y, x]);
                    }
                }
            }
        }
        
        #endregion

        #region Others

        public void Clear() => Array.Clear(_gridArray, 0, _gridArray.Length);

        #endregion
    }
}