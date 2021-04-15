using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
	// guarantee this will be always a singleton only - can't use the constructor!
	protected GameManager ()
	{

	}

	public bool Initialized = false;
	public bool Loaded = false;
	public ZPlayerSettings Settings;
	public GameFlow GameFlow;
	public EventManager EventManager;
	public bool allowQuit;
    public bool IsPlayerMorfing;
	
	public UserData Player {
		get {
			return Settings.User;
		}
	}
	
	public void Initialize ()
	{
		EventManager = new EventManager ();
		Settings = new ZPlayerSettings ();
		GameFlow = new GameFlow ();
		MusicManager.InitMusicManager();
        Localer.Init();
		Initialized = true;
        GameFlow.StartNewGame();
	}

	public void Load ()
	{
		Settings.Load ();
		Loaded = true;
	}

    [System.Obsolete]
    void OnApplicationQuit ()
	{
		allowQuit = true;
		if (allowQuit) 
		{
			//TODO: Записати налаштування
			Instance.GameFlow.ExitGame ();
		} 
		else 
		{
			Application.CancelQuit();
		}
	}
}