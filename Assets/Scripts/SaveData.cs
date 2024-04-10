using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public PlayerData playerData;
    public List<EnemyData> enemiesData = new List<EnemyData>();
    public InventoryData inventoryData;

    [Serializable]
    public class PlayerData
    {
        public int health;
        public float[] position;
    }

    [Serializable]
    public class EnemyData
    {
        public int id;
        public int health;
        public float[] position;
    }
}
