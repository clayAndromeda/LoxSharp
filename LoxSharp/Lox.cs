namespace LoxSharp;

public static class Lox
{
	public static bool HadError = false;
	
	/// <summary>
	/// スクリプトコード全体を渡して、スクリプトを実行する
	/// </summary>
	private static void Run(string source)
	{
		Scanner scanner = new(source);
		var tokens = scanner.ScanTokens();
		
		// 今はトークンを表示するだけ
		foreach (var token in tokens)
		{
			Console.WriteLine(token);
		}
	}

	/// <summary>
	/// スクリプトファイルのパスを指定してスクリプトを実行する
	/// </summary>
	public static void RunFile(string filePath)
	{
		var source = File.ReadAllText(filePath);
		Run(source);
		
		// 終了コードでエラーを通知する
		if (HadError) Environment.Exit(65);
	}

	/// <summary>
	/// REPLモードでスクリプトを実行する
	/// </summary>
	public static void RunPrompt()
	{
		// Read-Eval-Print Loop
		while (true)
		{
			Console.Write("> ");
			var line = Console.ReadLine();
			if (line == null) break;
			Run(line);
			HadError = false; // エラーフラグを戻す
		}
		Console.WriteLine("Goodbye!");
	}
	
	public static void Error(int line, string message)
	{
		Report(line, "", message);
	}
	
	private static void Report(int line, string where, string message)
	{
		Console.Error.WriteLine($"[line {line}] Error{where}: {message}");
		HadError = true;
	}
}