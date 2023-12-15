using System;

using MyAtsGame.GameClass;
using Game = MyAtsGame.GameClass.SfmlGameClass;
using Window = SFML.Graphics.RenderWindow;
namespace MyAtsGame.Scenes;
public class Loader : IScene
{
	public Exception? Load(ref Game game, in Window window)
	{
		return null;
	}
	public Exception? Update(ref Game game, long delta, out IScene? next)
	{
		next = null;
		return null;
	}
	public Exception? Draw(in Window window, long delta)
	{
		return null;
	}
	public Exception? Destroy(ref Game game, in Window window)
	{
		return null;
	}
}
