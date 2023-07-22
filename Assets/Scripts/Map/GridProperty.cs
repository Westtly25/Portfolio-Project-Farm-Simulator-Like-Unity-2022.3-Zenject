[System.Serializable]
public class GridProperty
{
    public GridCoordinate gridCoordinate;
    public GridBoolProperty gridBoolProperty;
    public bool gridBoolValue = false;

    public GridCoordinate GridCoordinate => gridCoordinate;
    public GridBoolProperty GridBoolProperty => gridBoolProperty;
    public bool GridBoolValue => gridBoolValue;

    public GridProperty(GridCoordinate gridCoordinate, GridBoolProperty gridBoolProperty, bool gridBoolValue)
    {
        this.gridCoordinate = gridCoordinate;
        this.gridBoolProperty = gridBoolProperty;
        this.gridBoolValue = gridBoolValue;
    }
}
