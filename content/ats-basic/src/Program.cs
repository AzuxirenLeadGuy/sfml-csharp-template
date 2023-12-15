using MyAtsGame.GameClass;

namespace MyAtsGame
{
	static class Program
	{
		static void Main()
		{
			GameConstants constants = new()
			{
				GameTitle = "My Game",
				Window = new()
				{
					Width = 800, Height = 600,
					Style = SFML.Window.Styles.Titlebar,
					FramePerSeconds = 60,
					VerticalSync = true,
				}
			};
			var exp = SfmlGameClass.RunGame(constants);
			if (exp != null)
				throw exp;
		}
	}
}
