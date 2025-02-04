namespace LoxSharp;

public class Token
{
    private TokenType Type { get; init; }
    private string Lexeme { get; init; }
    public object? Literal { get; init; }
    public int Line { get; init; }

    public Token(TokenType type, string lexeme, object? literal, int line)
    {
        Type = type;
        Lexeme = lexeme;
        Literal = literal;
        Line = line;
    }
    
    public override string ToString()
    {
        return $"{Type} {Lexeme} {Literal}";
    }
}