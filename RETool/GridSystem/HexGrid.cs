using UnityEngine;

namespace RETool.GridSystem;

public class HexGrid<T> : Grid2D<T>
{
    private const float HEX_VERTICAL_OFFSET_MULTIPLIER = .85f;
    
    public HexGrid(int width, int height) : base(width, height) { }

    #region World Vector

    public override Vector3 GetLocalPosition(int x, int y)
    {
        return new Vector3(x * (UnitGridSize + Gap.x), 0, 0) +
               new Vector3(0, 0, y * (UnitGridSize * HEX_VERTICAL_OFFSET_MULTIPLIER + Gap.y)) +
               (y % 2 == 1 ? Vector3.right * (UnitGridSize + Gap.x) * 0.5f : Vector3.zero);
    }


    public override void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        y = Mathf.FloorToInt(worldPosition.z / (UnitGridSize * HEX_VERTICAL_OFFSET_MULTIPLIER + Gap.y));
        var offset = (y % 2 == 1) ? (UnitGridSize + Gap.x) * 0.5f : 0;

        x = Mathf.FloorToInt((worldPosition.x - offset) / (UnitGridSize + Gap.x));
    }


    
    #endregion
}