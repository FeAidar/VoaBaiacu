using UnityEngine;
using System.IO;

public static class HighScoreManager
{
    static string FilePath =>
        Path.Combine(Application.persistentDataPath, "highscore.json");

    static HighScoreData _cached;

    public static int CurrentHighScore
    {
        get
        {
            if (_cached == null)
                Load();
            return _cached.highScore;
        }
    }

    public static void TrySave(int newScore)
    {
        if (newScore <= CurrentHighScore)
            return;

        _cached.highScore = newScore;
        Save();
    }

    static void Load()
    {
        if (!File.Exists(FilePath))
        {
            _cached = new HighScoreData { highScore = 0 };
            return;
        }

        var json = File.ReadAllText(FilePath);
        _cached = JsonUtility.FromJson<HighScoreData>(json);
    }

    static void Save()
    {
        var json = JsonUtility.ToJson(_cached);
        File.WriteAllText(FilePath, json);
    }
}