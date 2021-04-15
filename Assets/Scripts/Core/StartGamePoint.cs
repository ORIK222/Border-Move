using UnityEngine;
using System.Collections;

public class StartGamePoint : MonoBehaviour
{
	public UIConsts.SCENE_ID StartingScene;

	// Use this for initialization
	void Awake ()
	{
//		Application.targetFrameRate = 30;
		if (!GameManager.Instance.Initialized) {
			GameManager.Instance.Initialize ();
		}
		MusicManager.InitMusicManager ();
		if (!GameManager.Instance.Loaded) {
			GameManager.Instance.Load ();
		}
		LeanTween.init (1000);
	}

	void Start ()
	{
		if (StartingScene != UIConsts.SCENE_ID.NONE) {
			GameManager.Instance.GameFlow.TransitToScene (StartingScene);
		}
	}
}
