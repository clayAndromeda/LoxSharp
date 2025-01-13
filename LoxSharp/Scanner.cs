namespace LoxSharp;

/// <summary>
/// スクリプトをトークンに分解する字句解析器 (scanning, lexing)
/// </summary>
public class Scanner
{
    private readonly string _source;

    private int start = 0;
    private int current = 0;
    private int line = 1;
    private readonly List<Token> _tokens;

    public Scanner(string source)
    {
        _tokens = new List<Token>();
        _source = source;
    }

    /// <summary>
    /// トークンに分解する
    /// </summary>
    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            start = current;
            ScanToken();
        }

        // 終端トークンを追加
        _tokens.Add(new Token(TokenType.EOF, "", null, line));
        return _tokens;
    }

    private void ScanToken()
    {
        char c = Advance(); // トークンを1つ読み進める
        switch (c)
        {
            case '(': AddToken(TokenType.LEFT_PAREN); break;
            case ')': AddToken(TokenType.RIGHT_PAREN); break;
            case '{': AddToken(TokenType.LEFT_BRACE); break;
            case '}': AddToken(TokenType.RIGHT_BRACE); break;
            case ',': AddToken(TokenType.COMMA); break;
            case '.': AddToken(TokenType.DOT); break;
            case '-': AddToken(TokenType.MINUS); break;
            case '+': AddToken(TokenType.PLUS); break;
            case ';': AddToken(TokenType.SEMICOLON); break;
            case '*': AddToken(TokenType.STAR); break;

            case '!': AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
            case '=': AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
            case '<': AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
            case '>': AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;

            case '/':
                if (Match('/'))
                {
                    // コメント行をスキップする
                    while (Peek() != '\n' && !IsAtEnd()) Advance();
                }
                else
                {
                    AddToken(TokenType.SLASH);
                }

                break;
            case ' ':
            case '\r': 
            case '\t':
                break; // 空白は無視
            case '\n':
                line++; // 改行
                break;
            
            case '"': ScanString(); break;

            default: Lox.Error(line, "Unexpected character."); break;
        }
    }

    private void ScanString()
    {
        while (Peek() != '"' && !IsAtEnd())
        {
            if (Peek() == '\n') line++;
            Advance();
        }

        if (IsAtEnd())
        {
            Lox.Error(line, "Unterminated string."); // 文字列が終端していない
            return;
        }

        Advance(); // 右側の引用ふを消費する
        // 左右の引用符を切り捨てた残りの文字列をSTRINGトークンとして追加
        AddToken(TokenType.STRING, _source.Substring(start + 1, current - 1));
    }

    private char Peek()
    {
        // 一文字先読みする
        return IsAtEnd() ? '\0' : _source[current];
    }

    private bool Match(char expected)
    {
        // 次に期待した文字が入っていれば、読み進める
        if (IsAtEnd()) return false;
        if (_source[expected] != expected) return false;

        current++;
        return true;
    }

    private char Advance()
    {
        return _source[current++];
    }

    private void AddToken(TokenType type)
    {
        AddToken(type, null);
    }

    private void AddToken(TokenType type, object? literal)
    {
        string text = _source.Substring(start, current);
        _tokens.Add(new Token(type, text, literal, line));
    }


    private bool IsAtEnd()
    {
        return current >= _source.Length;
    }
}