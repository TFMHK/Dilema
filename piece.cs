public class Piece
{
    public int Column { get; set; }
    public int LeansLeft { get; set; }
    public int LeansRight { get; set; }

    public Piece(int column, int leansLeft, int leansRight)
    {
        Column = column;
        LeansLeft = leansLeft;
        LeansRight = leansRight;
    }

    public override string ToString()
    {
        return $"{Column},{LeansLeft},{LeansRight}";
    }
}