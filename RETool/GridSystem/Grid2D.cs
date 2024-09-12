using UnityEngine;

namespace RETool.GridSystem;

public class Grid2D<T> : Grid3D<T>
{
    public Grid2D(int width, int height) : base(1,width, height) { }

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
    }

    public override void SetValue(Vector3 worldPosition, T value)
    {
        GetXY(worldPosition, out var x,out var y);
        SetValue(x, y, value);
    }

    public override T? GetValue(Vector3 worldPosition)
    {
        GetXY(worldPosition, out var x, out var z);
        return GetValue(x, z);
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