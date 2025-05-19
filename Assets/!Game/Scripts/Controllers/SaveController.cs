using System.IO;
using UnityEngine;

namespace Game
{
    public static class SaveController
    {
        private static string SaveFilePath => Path.Combine(Application.persistentDataPath, "save.json");

        public static void Save()
        {
            var json = JsonUtility.ToJson(GameDataController.Instance.GameData);
            try
            {
                File.WriteAllText(SaveFilePath, json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Save error: {e.Message}");
            }
        }

        public static int Load()
        {
            if (!File.Exists(SaveFilePath))
            {
                return -1;
            }

            try
            {
                var json = File.ReadAllText(SaveFilePath);
                JsonUtility.FromJsonOverwrite(json, GameDataController.Instance.GameData);
                return 1;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Save load error: {e.Message}");
            }
            return -1;
        }
    }
}