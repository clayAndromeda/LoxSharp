namespace LoxSharp;

/// <summary>
/// スクリプトをトークンに分解する字句解析器 (scanning, lexing)
/// </summary>
public class Scanner(string source)
{
	private readonly string _source = source;

	/// <summary>
	/// トークンに分解する
	/// </summary>
	public List<Token> ScanTokens()
	{
		var tokens = new List<Token>();
		return tokens;
	}
}