using System;

class Program
{
    static void Main()
    {
        GameBoardManager manager = new GameBoardManager();

        // יצירת לוח משחק והוספת חתיכות
        Board board = new Board();
        board.AddPiece(new Piece(1, 2, 3), 0);
        board.AddPiece(new Piece(4, 5, 6), 1);

        // הוספת לוח משחק למנהל
        manager.AddGameBoard(board, "SomeValue");

        // חיפוש לוח משחק במנהל
        if (manager.TryGetGameBoard(board, out string value))
        {
            Console.WriteLine($"Found board with value: {value}");
        }
        else
        {
            Console.WriteLine("Board not found");
        }
    }
}