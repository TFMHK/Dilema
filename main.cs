using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // יצירת לוח משחק והוספת חתיכות עם מספרים שווים ל-9 או קטנים ממנו בכל חתיכה
        Board board = new Board();
        board.AddPiece(new Piece(1, 2, 3), 0);
        board.AddPiece(new Piece(2, 4, 5), 1);
        board.AddPiece(new Piece(3, 6, 7), 2);
        board.AddPiece(new Piece(4, 8, 9), 3);
        board.AddPiece(new Piece(5, 1, 2), 4);
        board.AddPiece(new Piece(6, 3, 4), 5);
        // השארת משבצת 6 ריקה

        // הדפסת הלוח למסך
        board.PrintBoard();

        // יצירת רשימת חתיכות להערכת הלוח עם לפחות שלוש חתיכות
        List<Piece> pieces = new List<Piece>
        {
            new Piece(7, 8, 9),
            new Piece(8, 1, 2),
            new Piece(9, 3, 4)
        };

        // בחירת המהלך הטוב ביותר
        Piece newPiece = new Piece(7, 8, 9);
        int bestMove = board.ChooseBestMove(newPiece, pieces);
        Console.WriteLine($"Best move for the new piece: {bestMove}");

        // הערכת הלוח
        double value = board.Evaluation(pieces.ToArray());
        Console.WriteLine($"Board evaluation: {value}");
    }
}
