using System.Collections.Generic;

[System.Serializable]
public class HighScoreData
{
    public List<HighScoreEntry> highscores;
}

[System.Serializable]
public class HighScoreEntry
{
    public int score;
    public string date; // formato ISO simples para leitura humana
}