using System;

using MyAtsGame.Scenes;

using SFML.Graphics;
using SFML.System;

namespace MyAtsGame.GameClass
{
	public partial class SfmlGameClass(GameConstants constants)
	{
		public readonly GameConstants Constants = constants;
		protected RenderWindow _window = new
		(
			new
			(
				constants.Window.Width,
				constants.Window.Height
			),
			constants.GameTitle,
			constants.Window.Style
		);
		protected Clock _clock = new();
		protected bool _exit_called;
		protected Exception? Initialize(out IScene foreground, out IScene background)
		{
			foreground = new Starter();
			background = new Loader();
			if (Constants.Window.VerticalSync)
				_window.SetVerticalSyncEnabled(true);
			else
				_window.SetFramerateLimit(Constants.Window.FramePerSeconds);
			_window.Closed += (o, e) => _exit_called = true;
			// TODO Add more initialization to game if needed
			return null;
		}
		protected Exception? OnBeginFrame()
		{
			_window.DispatchEvents();
			_window.Clear();
			// TODO Other startup if needed
			return null;
		}
		protected Exception? OnEndFrame(out long time, out bool exit_called)
		{
			_window.Display();
			// TODO Other actions if needed
			time = _clock.Restart().AsMicroseconds();
			exit_called = _exit_called;
			return null;
		}
		protected Exception? ShutDown()
		{
			_window.Close();
			_window.Dispose();
			_clock.Dispose();
			// TODO Other cleanup if needed
			return null;
		}
		public bool ExitCall()
		{
			bool prev = _exit_called;
			_exit_called = true;
			return prev;
		}
	}
}
