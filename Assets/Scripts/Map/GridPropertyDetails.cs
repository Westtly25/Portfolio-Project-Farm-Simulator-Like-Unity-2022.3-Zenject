[System.Serializable]
public sealed class GridPropertyDetails
{
    public int GridX { get; set; }
    public int GridY { get; set; }
    public bool IsDiggable { get; set; } = false;
    public bool CanDropItem { get; set; } = false;
    public bool CanPlaceFurniture { get; set; } = false;
    public bool IsPath { get; set; } = false;
    public bool IsNPCObstacle { get; set; } = false;
    public int DaysSinceDug { get; set; } = -1;
    public int DaysSinceWatered { get; set; } = -1;
    public int SeedItemCode { get; set; } = -1;
    public int GrowthDays { get; set; } = -1;
    public int DaysSinceLastHarvest { get; set; } = -1;

    public GridPropertyDetails()
    {
    }
}
