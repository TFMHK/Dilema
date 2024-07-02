using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public class GameBoardManager
{
    private string filePath = "gameBoards.json";
    private Dictionary<string, double> gameBoards = new Dictionary<string, double>();

    public GameBoardManager()
    {
        LoadGameBoards();
    }

    private void LoadGameBoards()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameBoards = JsonConvert.DeserializeObject<Dictionary<string, double>>(json) ?? new Dictionary<string, double>();
        }
    }

    private void SaveGameBoards()
    {
        string json = JsonConvert.SerializeObject(gameBoards, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    private string ComputeHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public void AddGameBoard(Board board, double value)
    {
        string boardString = board.ToString();
        string hash = ComputeHash(boardString);
        if (!gameBoards.ContainsKey(hash))
        {
            gameBoards[hash] = value;
            SaveGameBoards();
        }
    }

    public bool TryGetGameBoard(Board board, out double value)
    {
        string boardString = board.ToString();
        string hash = ComputeHash(boardString);
        return gameBoards.TryGetValue(hash, out value);
    }
}