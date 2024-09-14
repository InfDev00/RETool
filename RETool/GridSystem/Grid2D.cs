using UnityEngine;

namespace RETool.GridSystem;

public class Grid2D<T> : Grid3D<T>
{
    #region Constructor

    public Grid2D(int width, int height) : base(1,width, height) { }
    public Grid2D(int size) : base(1,size,size) { }

    #endregion

    #region Coordinate

    public void SetValue(int x, int y, T value, bool warning = false) => base.SetValue(x, y, 0, value, warning);

    public T? GetValue(int x, int y, bool warning = false) => base.GetValue(x, y, 0, warning);

    public void ForEach(Action<int, int, T> action)
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                action(x, y, _gridArray[0, y, x]);
            }
        }
    }
    public bool IsValidCoordinate(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

    #endregion

    #region Vector

    public virtual Vector3 GetLocalPosition(int x, int y)
    {
        //Get the Center position of each grid
        return new Vector3(
            x * (UnitGridSize + Gap.x) + UnitGridSize / 2,
            0,
            y * (UnitGridSize + Gap.y) + UnitGridSize / 2
        );
    }
    
    public virtual void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x - UnitGridSize / 2) / (UnitGridSize + Gap.x));
        y = Mathf.FloorToInt((worldPosition.z - UnitGridSize / 2) / (UnitGridSize + Gap.z));
        
        if (!IsValidCoordinate(x, y)) throw new IndexOutOfRangeException("Given Vector3 is out of range");
    }

    public override void SetValue(Vector3 worldPosition, T value, bool warning = false)
    {
        GetXY(worldPosition, out var x,out var y);
        SetValue(x, y, value, warning);
    }

    public override T? GetValue(Vector3 worldPosition, bool warning = false)
    {
        GetXY(worldPosition, out var x, out var z);
        return GetValue(x, z, warning);
    }
    
    public override void ForEach(Action<Vector3, T> action)
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                action(GetLocalPosition(x, y), _gridArray[0, y, x]);
            }
        }
    }
    
    #endregion
}