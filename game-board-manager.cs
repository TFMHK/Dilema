using System;
using System.Collections.Generic;
using System.IO;

public class GameBoardManager
{
    private Dictionary<string, double> boardEvaluations;
    private const string FileName = "boardEvaluations.txt";

    public GameBoardManager()
    {
        boardEvaluations = new Dictionary<string, double>();
        LoadEvaluationsFromFile();
    }

    public bool TryGetGameBoard(Board board, out double evaluation)
    {
        return boardEvaluations.TryGetValue(board.ToString(), out evaluation);
    }

    public void AddGameBoard(Board board, double evaluation)
    {
        string boardStr = board.ToString();
        if (!boardEvaluations.ContainsKey(boardStr))
        {
            boardEvaluations[boardStr] = evaluation;
            SaveEvaluationsToFile();
        }
    }

    private void LoadEvaluationsFromFile()
    {
        if (File.Exists(FileName))
        {
            foreach (var line in File.ReadAllLines(FileName))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && double.TryParse(parts[1], out double evaluation))
                {
                    boardEvaluations[parts[0]] = evaluation;
                }
            }
        }
    }

    private void SaveEvaluationsToFile()
    {
        using (var writer = new StreamWriter(FileName))
        {
            foreach (var entry in boardEvaluations)
            {
                writer.WriteLine($"{entry.Key}|{entry.Value}");
            }
        }
    }
}