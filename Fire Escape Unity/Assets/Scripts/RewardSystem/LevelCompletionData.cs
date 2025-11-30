using UnityEngine;

[System.Serializable]
    public class LevelResultData
    {
        public string levelName;

        public int humansSaved;
        public int totalHumans;

        public float fireExtinguishedPercent;

        public int bonusCollected;
        public int bonusTotal;
    }

    public static class LevelResultCache
    {
        public static LevelResultData Data;
    }