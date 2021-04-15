//=====================================================================================
//
// Game Flow
// Description: Class uses to transit between scenes, create and manage UI elements
//
// Example: GameManager.Instance.GameFlow.TransitToScene("MainMenu");
// 
// Company: ZagravaGames
// 2015/03
//=====================================================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class SceneData
{
	public string Name;
	public string Menu;
	public UIConsts.SCENE_ID PreloadSceneName;

	public SceneData (string name)
	{
		Menu = "";
		Name = name;
		PreloadSceneName = UIConsts.SCENE_ID.NONE;
	}
}

public class GameFlow
{
	private UIConsts.SCENE_ID _currentScene;
	private string _currentBackgroundMenu;
	private bool _isSomeForm;
	private Dictionary<UIConsts.SCENE_ID, AsyncOperation> _scenePreloadersOperations;

	//
	public static GameObject CurrentActiveWindow;
	public static GameObject CurrentActivePopUp;
	public static GameObject BaseWindow;
	public static string SceneToTransit;
    private bool _onPause;
    public bool GamePaused {
        get {
            return _onPause;
        }
        set {
            _onPause = value;
            if (_onPause) {
                EventData eventData = new EventData ("OnPauseGameEvent");
                GameManager.Instance.EventManager.CallOnPauseGameEvent (eventData);
            } else {
                EventData eventData = new EventData ("OnUnPauseGameEvent");
                GameManager.Instance.EventManager.CallOnUnPauseGameEvent (eventData);
            }
        }
    }
	//

	public GameFlow ()
	{
		_scenePreloadersOperations = new Dictionary<UIConsts.SCENE_ID, AsyncOperation> ();
	}

	public UIConsts.SCENE_ID GetCurrentScene ()
	{
		return _currentScene;
	}

	public void TransitToScene (UIConsts.SCENE_ID nextScene)
	{
		if (_currentScene != UIConsts.SCENE_ID.SPLASHSCENE) {
			var fader = new FadeTransition () {
                fadeToColor = Color.black
            };
			TransitionKit.instance.transitionWithDelegate (fader);
		}

		GameManager.Instance.StartCoroutine (PreloadSceneAsync (nextScene));

		LeanTween.delayedCall (0.5f, () => TransitToSceneDelayed (nextScene));
	}

	private void TransitToSceneDelayed (UIConsts.SCENE_ID nextScene)
	{
		_currentScene = nextScene;

		AsyncOperation preloadAsyncOperation;
		if (_scenePreloadersOperations.TryGetValue (nextScene, out preloadAsyncOperation)) {
			preloadAsyncOperation.allowSceneActivation = true;
			_scenePreloadersOperations.Remove (nextScene);
		} else {
			_scenePreloadersOperations.Clear ();
			//Debug.Log ("GameFlow: transiting to scene <" + nextScene + ">");
			SceneManager.LoadScene (UIConsts.SCENE_NAMES [(int)nextScene]);
		}
	}

	private IEnumerator PreloadSceneAsync (UIConsts.SCENE_ID sceneToPreload)
	{
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync (UIConsts.SCENE_NAMES [(int)sceneToPreload]);
		asyncOperation.allowSceneActivation = false;
		EventData eventData = new EventData("OnAsyncSceneLoadEvent");
		eventData.Data.Add("asyncOperation", asyncOperation);
		GameManager.Instance.EventManager.CallOnAsyncSceneLoadEvent(eventData);
		_scenePreloadersOperations.Add (sceneToPreload, asyncOperation);
		yield return asyncOperation;
		//Debug.Log ("Preload done ____________________________");
	}

	private UIConsts.SCENE_ID convertSceneNameToID (string sceneName)
	{
		for (int i = 0; i <= UIConsts.SCENE_NAMES.Length; i++) {
			if (UIConsts.SCENE_NAMES [i] == sceneName) {
				return (UIConsts.SCENE_ID)i;
			}
		}
		return UIConsts.SCENE_ID.NONE;
	}

    private void ReloadCurrentScene() {
        GameManager.Instance.IsPlayerMorfing = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void ExitGame ()
	{
		GameManager.Instance.Settings.Save ();
		Application.Quit ();
	}

	public void StartNewGame ()
	{
        //Time.timeScale = 1;
		MusicManager.PlayTrack("main", 0.1f);
	}

    public void RestartGame ()
    {
        //Time.timeScale = 1;
        ReloadCurrentScene();
    }

	public void LooseGame ()
	{
        Debug.Log("Loose Game");
        //Time.timeScale = Mathf.Epsilon;
		MusicManager.playSingleSound ("fail");
        EventData eventData = new EventData("OnLostGameEvent");
        GameManager.Instance.EventManager.CallOnLostGameEvent(eventData);
	}

	public void WinGame ()
	{
        //Time.timeScale = Mathf.Epsilon;
		MusicManager.playSingleSound ("win2");
        EventData eventData = new EventData("OnWinGameEvent");
        GameManager.Instance.EventManager.CallOnWinGameEvent(eventData);
	}

	public void PauseGame ()
	{
		if (!GamePaused) {
			GamePaused = true;
			//Time.timeScale = Mathf.Epsilon;

			EventData eventData = new EventData ("OnPauseGameEvent");
			GameManager.Instance.EventManager.CallOnPauseGameEvent (eventData);
		}
	}
}
