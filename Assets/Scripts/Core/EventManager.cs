using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager
{
    public static event EventController.MethodContainer OnShowWindowEvent;
    public void CallOnShowWindowEvent(EventData ob = null) { if (OnShowWindowEvent != null) OnShowWindowEvent(ob); }

    public static event EventController.MethodContainer OnHideWindowEvent;
    public void CallOnHideWindowEvent(EventData ob = null) { if (OnHideWindowEvent != null) OnHideWindowEvent(ob); }

	public static event EventController.MethodContainer OnAsyncSceneLoadEvent;
	public void CallOnAsyncSceneLoadEvent(EventData ob = null) { if (OnAsyncSceneLoadEvent != null) OnAsyncSceneLoadEvent(ob); }

    public static event EventController.MethodContainer OnLocaleChangedEvent;
    public void CallOnLocaleChangedEvent(EventData ob = null) { if (OnLocaleChangedEvent != null) OnLocaleChangedEvent(ob); }

    public static event EventController.MethodContainer OnWinGameEvent;
    public void CallOnWinGameEvent(EventData ob = null) { if (OnWinGameEvent != null) OnWinGameEvent(ob); }

    public static event EventController.MethodContainer OnLostGameEvent;
	public void CallOnLostGameEvent(EventData ob = null) { if (OnLostGameEvent != null) OnLostGameEvent(ob); }

	public static event EventController.MethodContainer OnPauseGameEvent;
	public void CallOnPauseGameEvent(EventData ob = null) { if (OnPauseGameEvent != null) OnPauseGameEvent(ob); }

    public static event EventController.MethodContainer OnUnPauseGameEvent;
    public void CallOnUnPauseGameEvent(EventData ob = null) { if (OnUnPauseGameEvent != null) OnUnPauseGameEvent(ob); }

    public static event EventController.MethodContainer OnShakeCameraEvent;
	public void CallOnShakeCameraEvent(EventData ob = null) { if (OnShakeCameraEvent != null) OnShakeCameraEvent(ob); }
}
