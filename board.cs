using System;
using System.Text;
using System.Collections.Generic;

public class Board
{
    private Piece[] pieces;
    private static GameBoardManager manager = new GameBoardManager();
    private static Random random = new Random();

    public Board()
    {
        pieces = new Piece[7]; 
        for (int i = 0; i < pieces.Length; ++i)
        {
            pieces[i] = null;
        }
    }

    public Board(Board board)
    {
        pieces = new Piece[7];
        for (int i = 0; i < pieces.Length; ++i)
        {
            pieces[i] = board.pieces[i];
        }
    }

    public Board AddPiece(Piece piece, int square)
    {
        if (pieces[square] == null)
        {
            pieces[square] = piece;
        }
        else
        {
            throw new Exception("Square is already taken");
        }
        return this;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Piece piece in pieces)
        {
            sb.Append(piece?.ToString() ?? "null").Append(";");
        }
        return sb.ToString();
    }

    public double Evaluation(Piece[] pieces)
    {
        if (manager.TryGetGameBoard(this, out double existingValue))
        {
            return existingValue;
        }

        int numOfEmptySquares = 0;
        double value = 0;
        double valOfSquare = 0;

        foreach (var piece in this.pieces)
        {
            if (piece == null)
            {
                ++numOfEmptySquares;
                foreach (var newPiece in pieces)
                {
                    valOfSquare += new Board(this).AddPiece(newPiece, Array.IndexOf(this.pieces, piece)).Evaluation(pieces);
                }
                valOfSquare = valOfSquare / pieces.Length;
            }
            value += valOfSquare;
            valOfSquare = 0;
        }

        if (numOfEmptySquares == 0)
        {
            value = EvaluationHalper();
        }
        else
        {
            value = value / numOfEmptySquares;
        }
        manager.AddGameBoard(this, value);
        return value;
    }

    private double EvaluationHalper()
    {
        int calcColumns = 0;
        calcColumns += CheckAndCalc(0, 1);
        calcColumns += CheckAndCalc(2, 3, 4);
        calcColumns += CheckAndCalc(5, 6);

        int calcLeansLeft = 0;
        calcLeansLeft += CheckAndCalc(0, 2);
        calcLeansLeft += CheckAndCalc(1, 3, 5);
        calcLeansLeft += CheckAndCalc(4, 6);

        int calcLeansRight = 0;
        calcLeansRight += CheckAndCalc(2, 5);
        calcLeansRight += CheckAndCalc(0, 3, 6);
        calcLeansRight += CheckAndCalc(1,4);

        return calcColumns + calcLeansLeft + calcLeansRight;
    }

    private int CheckAndCalc(int first, int second, int third = -1)
    {
        if (pieces[first] != null && pieces[first] == pieces[second] && (third == -1 || pieces[first] == pieces[third]))
        {
            int numInRow = third == -1 ? 2 : 3;
            return pieces[first].Column * numInRow;
        }
        return 0;
    }

    public int ChooseBestMove(Piece piece, List<Piece> remainingPieces)
    {
        double bestScore = double.MinValue;
        int bestMove = -1;
        int[] validSquares = { 0, 1, 2, 3, 4, 5, 6 };

        foreach (int i in validSquares)
        {
            if (pieces[i] == null)
            {
                Board newBoard = new Board(this);
                newBoard.AddPiece(piece, i);
                double score = newBoard.Evaluation(remainingPieces.ToArray());

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = i;
                }
            }
        }

        return bestMove;
    }

    public void PrintBoard()
    {
        string[] display = new string[9];
        display[0] = "  \\ 1 /  ";
        display[1] = "0 \\ / 2  ";
        display[2] = "  / \\    ";
        display[3] = " / 3 \\   ";
        display[4] = "|     |  ";
        display[5] = " \\ 4 / 5 ";
        display[6] = "  \\ / 6  ";

        for (int i = 0; i < pieces.Length; ++i)
        {
            if (pieces[i] != null)
            {
                int row = i switch
                {
                    0 => 0,
                    1 => 0,
                    2 => 1,
                    3 => 3,
                    4 => 5,
                    5 => 5,
                    6 => 6,
                    _ => -1,
                };

                int col = i switch
                {
                    0 => 2,
                    1 => 5,
                    2 => 7,
                    3 => 2,
                    4 => 4,
                    5 => 7,
                    6 => 7,
                    _ => -1,
                };

                string pieceStr = $"{pieces[i].Column},{pieces[i].LeansLeft},{pieces[i].LeansRight}";
                display[row] = display[row].Remove(col, pieceStr.Length).Insert(col, pieceStr);
            }
        }

        foreach (string line in display)
        {
            Console.WriteLine(line);
        }
    }
}