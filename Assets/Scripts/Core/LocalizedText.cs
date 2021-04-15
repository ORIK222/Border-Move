using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
	public string TextID = "";

	void Start ()
	{
		SetText();
	}

	void SetText()
	{
		Text text = GetComponent<Text> ();
		if (text != null) {
			if (TextID != "") {
				text.text = Localer.GetText (TextID);
			}
		} else {
			Debug.LogError ("Can't set localized text: Text component not found in " + gameObject.name);
		}
	}

	public virtual void OnLocaleChanged (EventData ob = null)
	{
		SetText();
	}

	void Awake()
	{
		EventManager.OnLocaleChangedEvent += OnLocaleChanged;
	}

	void OnDestroy()
	{
		EventManager.OnLocaleChangedEvent -= OnLocaleChanged;
	}

}
