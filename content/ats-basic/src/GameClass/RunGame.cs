using System;
using System.Threading.Tasks;
namespace MyAtsGame.GameClass;

/// <summary>
/// The Abstract SFMLGame class that needs to be implemented
/// </summary>
public partial class SfmlGameClass
{
	public static Exception? RunGame(in GameConstants constants)
	{
		SfmlGameClass game = new(constants);
		Exception? exp = game.Initialize(out var main, out var back);
		if (exp != null) { return exp; }
		else if ((exp = main.Load(
						ref game,
						in game._window)) != null) { return exp; }
		else if ((exp = back.Load(
						ref game,
						in game._window)) != null) { return exp; }

		Exception? loading_exception = null;
		bool is_loading = false, exit_called;
		long time = 0;
		do
		{
			var cur_scene = is_loading ? back : main;
			if ((exp = game.OnBeginFrame()) != null) { return exp; }
			else if ((exp = cur_scene.Update(
								ref game,
								time,
								out var next)) != null) { return exp; }
			else if ((exp = cur_scene.Draw(
								in game._window, time)) != null) { return exp; }
			else if ((exp = game.OnEndFrame(out time, out exit_called)) != null) { return exp; }
			else if (loading_exception != null) { return loading_exception; }
			else if (is_loading) { continue; }
			else if (next != null)
			{
				is_loading = true;
				Task.Run(() =>
				{
					loading_exception = main.Destroy(ref game, in game._window);
					if (loading_exception != null) return;
					main = next;
					loading_exception = main.Load(ref game, in game._window);
					is_loading = false;
				});
			}
		} while (!exit_called);
		return main.Destroy(ref game, in game._window) ??
			back.Destroy(ref game, in game._window) ??
			game.ShutDown();
	}
}
