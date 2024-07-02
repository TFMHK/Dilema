using System;
using System.Text;

public class Board
{
    private Piece[] pieces;
    private static GameBoardManager manager = new GameBoardManager();

    public Board()
    {
        pieces = new Piece[19];
        for (int i = 0; i < 19; ++i)
        {
            pieces[i] = null;
        }
    }

    public Board(Board board)
    {
        pieces = new Piece[19];
        for (int i = 0; i < 19; ++i)
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

        if(numOfEmptySquares == 0)
        {
            value = EvaluationHalper();
        }
        else{
            value = value / numOfEmptySquares;
        }
        manager.AddGameBoard(this, value);
        return value;
    }

    private double EvaluationHalper()
    {
        int calcColumns = 0;
        //firs column
        calcColumns = chackAndCalc(0,1,2);
        //second column
        calcColumns = chackAndCalc(3,4,5,6);
        //third column
        calcColumns = chackAndCalc(7,8,9,10,11);
        //fourth column
        calcColumns = chackAndCalc(12,13,14,15);
        //fith column
        calcColumns = chackAndCalc(16,17,18);

        int calcLeansLeft = 0;
        //firs leansLeft
        calcLeansLeft = chackAndCalc(0,4,2);
        //second leansLeft
        calcLeansLeft = chackAndCalc(3,4,5,6);
        //third leansLeft
        calcLeansLeft = chackAndCalc(7,8,9,10,11);
        //fourth leansLeft
        calcLeansLeft = chackAndCalc(12,13,14,15);
        //fith leansLeft
        calcLeansLeft = chackAndCalc(16,17,18);

    }

    private int chackAndCalc(int first, int second, int third, int fourth = first, int fith = first)
    {
        if(pieces[first] == pieces[second] && pieces[first] == pieces[third] && pieces[first] == pieces[fourth] && pieces[first] == pieces[fith])
            return pieces[first];
        return 0;
    }
}