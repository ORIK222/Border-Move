using UnityEngine;
using System.Collections;
using System;

public class UIConsts
{
	public static Color POPUP_TEXT_COLOR = new Color (0.2f, 0.18f, 0.13f);
	public static Vector3 REFERENCE_RESOLUTION = new Vector2 (1366, 768);
	public static Vector3 START_POSITION = new Vector3 (1366, 0f, 0f);
	public static Vector3 STOP_POSITION = new Vector3 (0f, 0f, 0f);
	public static LeanTweenType SHOW_EASE = LeanTweenType.easeOutBack;
	public static LeanTweenType HIDE_EASE = LeanTweenType.easeInBack;
	public static float SHOW_DELAY_TIME = 0.0f;
	public static float HIDE_DELAY_TIME = 0.0f;
	public static float SHOW_TWEEN_TIME = 0.5f;
	public static float HIDE_TWEEN_TIME = 0.5f;
	public static float SCROLL_TIME = 0.2f;
	public static LeanTweenType SCROLL_EASE = LeanTweenType.easeOutCubic;
	public static bool ENABLED_INTERACTABLE = false;
	public static float ANTI_MULTI_CKLICK_TIMEOUT = 0.3f;
	public static float MAX_MUSIC_VOLUME = 0.3f;
	public static float MAX_SOUND_VOLUME = 0.3f;

	public enum SCENE_ID
	{
		NONE = -1,
		SPLASHSCENE = 0,
		MENU = 1,
		TWOPLAYERLEVEL = 2,
		SINGLEPLAYERLEVEL = 3
	}

	public static string[] SCENE_NAMES = {
		"SplashScene",
		"Menu",
		"TwoPlayerLevel",
		"SinglePlayerLevel"
	};
}

