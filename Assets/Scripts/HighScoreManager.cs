using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

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

            if (_cached.highscores == null || _cached.highscores.Count == 0)
                return 0;

            return _cached.highscores[0].score; // já vem ordenado
        }
    }

    public static void TrySave(int newScore)
    {
        if (_cached == null)
            Load();

        if (newScore < 500) // tua regra original
            return;

        // cria nova entrada com data
        var entry = new HighScoreEntry
        {
            score = newScore,
            date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        // adiciona
        _cached.highscores.Add(entry);

        // ordena (maior primeiro)
        _cached.highscores.Sort((a, b) => b.score.CompareTo(a.score));

        // limita a 10
        if (_cached.highscores.Count > 10)
            _cached.highscores.RemoveRange(10, _cached.highscores.Count - 10);

        Save();
    }

    static void Load()
    {
        if (!File.Exists(FilePath))
        {
            _cached = new HighScoreData { highscores = new List<HighScoreEntry>() };
            return;
        }

        var json = File.ReadAllText(FilePath);
        _cached = JsonUtility.FromJson<HighScoreData>(json);

        if (_cached.highscores == null)
            _cached.highscores = new List<HighScoreEntry>();
    }

    static void Save()
    {
        var json = JsonUtility.ToJson(_cached, true);
        File.WriteAllText(FilePath, json);
    }
}
