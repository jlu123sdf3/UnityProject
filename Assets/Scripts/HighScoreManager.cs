using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace HighScore
{
    public class HighScoreManager
    {
        [Serializable]
        public class HighScoreEntry
        {
            public string playerName;
            public int score;
        }

        [Serializable]
        public class HighScoreData
        {
            public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
        }

        private static string filePath = UnityEngine.Application.persistentDataPath + "/highscores.json";

        public static void SaveHighScores(HighScoreData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
            UnityEngine.Debug.Log($"High scores saved to {filePath}");
        }

        public static HighScoreData LoadHighScores()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<HighScoreData>(json);
            }
            else
            {
                UnityEngine.Debug.Log("No high score file found, creating new one.");
                return new HighScoreData();
            }
        }

        public static void AddHighScore(string playerName, int score)
        {
            HighScoreData data = LoadHighScores();

            data.highScores.Add(new HighScoreEntry { playerName = playerName, score = score });

            data.highScores.Sort((a, b) => b.score.CompareTo(a.score));

            if (data.highScores.Count > 10)
            {
                data.highScores = data.highScores.GetRange(0, 10);
            }

            SaveHighScores(data);
        }
    }
}