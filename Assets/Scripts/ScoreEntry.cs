using System.Collections.Generic;

[System.Serializable]
public class ScoreEntry
{
    public string Name;
    public int Score;
}

[System.Serializable]
public class ScoreList
{
    public List<ScoreEntry> Scores = new();
}
