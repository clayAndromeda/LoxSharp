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

	public Scanner(string source)
	{
		_source = source;
	}

	/// <summary>
	/// トークンに分解する
	/// </summary>
	public List<Token> ScanTokens()
	{
		var tokens = new List<Token>();
		while (!IsAtEnd())
		{
		}
		return tokens;
	}

	private bool IsAtEnd()
	{
		return current >= _source.Length;
	}
}