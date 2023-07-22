using UnityEngine;

[System.Serializable]
public sealed class GridCoordinate
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    public int X => x;
    public int Y => y;

    public GridCoordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static explicit operator Vector2(GridCoordinate gridCoordinate) =>
        new Vector2((float)gridCoordinate.X, (float)gridCoordinate.Y);

    public static explicit operator Vector2Int(GridCoordinate gridCoordinate) =>
        new Vector2Int(gridCoordinate.X, gridCoordinate.Y);

    public static explicit operator Vector3(GridCoordinate gridCoordinate) =>
        new Vector3((float)gridCoordinate.X, (float)gridCoordinate.Y, 0f);

    public static explicit operator Vector3Int(GridCoordinate gridCoordinate) =>
        new Vector3Int(gridCoordinate.X, gridCoordinate.Y, 0);
}