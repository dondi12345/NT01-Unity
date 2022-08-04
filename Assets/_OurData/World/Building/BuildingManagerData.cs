using System;
using System.Collections.Generic;

[Serializable]
public class BuildingManagerData
{
    public WarehouseData warehouseData;
    public MiningBuildingData miningBuildingData;

    //MoneyBuilding
    public List<BuildingData> buildingDatas;
}
