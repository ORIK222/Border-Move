using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class BaseUIController : MonoBehaviour {

	public bool Active = false;
	public bool RootUIController = true;
	public bool _isOver;
	private EventTrigger _eventTrigger;
	public bool ClickOutside = false;
	private bool inMotion = false;
	private RectTransform _rectTransform;
	private RectTransform myRect {
		get {
			if (_rectTransform == null) {
				_rectTransform = gameObject.GetComponent<RectTransform>();
			}
			return _rectTransform;
		}
		set {
			_rectTransform = value;
		}
	}

    void Awake() {
        myRect.anchoredPosition3D = UIConsts.START_POSITION;
        ReInit();
    }

	void Update() {
		UpdateInput();
	}

	public virtual bool OpenForm(EventData e)
	{
		// example
		//_atype = (string)e.Data["type"];
		return true;
	}

	private void AddEventTrigger(UnityAction action, EventTriggerType triggerType)
	{
		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		trigger.AddListener((eventData) => action());
		
		EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };
		
		_eventTrigger.triggers.Add(entry);
	}
	
	private void OnPointerEnter()
	{
		_isOver = true;
	}
	
	private void OnPointerExit()
	{
		//gameObject.GetComponent<RectTransform>().anchoredPosition3D = UIConsts.START_POSITION;
		_isOver = false;
	}

	protected virtual void UpdateInput()
	{
		//TODO тут проверяем есть ли клик за окном это тест и он работает надо прикрепить к окнам это дело
		//if (ClickOutside && Active && Input.GetMouseButtonDown(0) && !_isOver )
		if (ClickOutside && !_isOver && Input.GetMouseButtonDown(0))
		{
			Hide();
		}
	}

	public virtual void Reset()
	{

	}

	public virtual void Show ()
	{
        gameObject.SetActive(true);
        if (myRect.anchoredPosition3D != UIConsts.STOP_POSITION && inMotion == false) {
			Reset ();

			inMotion = true;

            float delayBeforeShow = UIConsts.SHOW_DELAY_TIME;

            LeanTween.delayedCall(delayBeforeShow, () => {MusicManager.playSound("menu_slide_in");});

            LeanTween.value(gameObject, UIConsts.START_POSITION, UIConsts.STOP_POSITION, UIConsts.SHOW_TWEEN_TIME)
                .setIgnoreTimeScale(true)
                .setEase(UIConsts.SHOW_EASE)
                .setDelay(delayBeforeShow)
                .setOnUpdate(
                    (Vector3 val)=>
                    {
                        myRect.anchoredPosition3D = val;
                    })
					.setOnComplete(() => {inMotion = false;});
		}
	}
	
	public virtual void Hide ()
	{
		if (myRect.anchoredPosition3D != UIConsts.START_POSITION && inMotion == false) {
			GameManager.Instance.EventManager.CallOnHideWindowEvent ();
			inMotion = true;
            MusicManager.playSound("menu_slide_in");
            LeanTween.value (gameObject, UIConsts.STOP_POSITION, UIConsts.START_POSITION, UIConsts.HIDE_TWEEN_TIME)
			.setEase (UIConsts.HIDE_EASE)
                .setIgnoreTimeScale(true)
				.setDelay (UIConsts.HIDE_DELAY_TIME)
				.setOnUpdate (
                    (Vector3 val) => {
						myRect.anchoredPosition3D = val; })
			    .setOnComplete( () => {
				    inMotion = false;
				    gameObject.SetActive (false); }
			);
		}
	}

	void OnEventShowWindow(EventData ob = null)
	{
		if (RootUIController)
		{
			ActivateDeActivate();
		}
	}

	void OnEventHideWindow(EventData ob = null)
	{
		if(RootUIController)
		{
			GameFlow.CurrentActiveWindow = GameFlow.BaseWindow;
			ActivateDeActivate();
		}
	}

	void ActivateDeActivate()
	{
		if (!gameObject.activeSelf) return;
		if (GameFlow.BaseWindow != gameObject) return;
		if (GameFlow.CurrentActiveWindow != gameObject)
		{
			Active = false;
			BroadcastMessage("Disable");
			OnDeactivate();
		}
		else
		{
			Active = true;
			BroadcastMessage("Enable");
			OnActivate();
		}
	}

	protected virtual void OnDeactivate()
	{

	}

	protected virtual void OnActivate()
	{
		
	}

	protected virtual void OnEnable()
	{
		EventManager.OnShowWindowEvent += OnEventShowWindow;
		EventManager.OnHideWindowEvent += OnEventHideWindow;
	}

    protected virtual void OnDisable()
	{
		EventManager.OnShowWindowEvent -= OnEventShowWindow;
		EventManager.OnHideWindowEvent -= OnEventHideWindow;
	}

	public virtual void ReInit()
	{
		_eventTrigger = gameObject.AddComponent<EventTrigger>();
		if(_eventTrigger.triggers == null)	{ _eventTrigger.triggers = new List<EventTrigger.Entry>();	}
		
		AddEventTrigger(OnPointerEnter, EventTriggerType.PointerEnter);
		AddEventTrigger(OnPointerExit, EventTriggerType.PointerExit);
	}

}
