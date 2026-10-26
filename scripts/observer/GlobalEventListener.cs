using Godot;
using System.Collections.Generic;

public partial class GlobalEventListener : Node
{
	public static GlobalEventListener Instance { get; private set; }

	private readonly List<IGameObserver> observers = new();

	public override void _Ready()
	{
		Instance = this;
	}

	public void RegisterObserver(IGameObserver observer)
	{
		if (!observers.Contains(observer))
		{
			observers.Add(observer);
		}
	}

	public void RemoveObserver(IGameObserver observer)
	{
		observers.Remove(observer);
	}

	public void NotifyObservers(GameEventType gameEvent)
	{
		foreach (IGameObserver observer in observers.ToArray())
		{
			observer.OnNotify(gameEvent);
		}
	}

	public override void _ExitTree()
	{
		observers.Clear();

		if (Instance == this)
		{
			Instance = null;
		}
	}
}
