using System.Collections.Generic;


[System.Serializable]
public class InventoryData
{
    public List<int> itemIDs;
    public List<int> quantities;

    public InventoryData(List<int> itemIDs, List<int> quantities)
    {
        this.itemIDs = itemIDs;
    
        this.quantities = quantities;
    }
}
