using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TObject.Shared;

public class Localer : MonoBehaviour
{
	private static Dictionary<string,string> _textBase;
	public static List<string> AllLanguages;
	public static string CurrentLocale = "English";
	
	public static void Init (string locale = "English")
	{
		_textBase = new Dictionary<string,string> ();
		AllLanguages = new List<string> { "English", "Ukrainian", "Russian", "Deutch" };
//		Reload (GetSystemLanguage ());
		Reload (locale);
	}
	
	public static void Reload (string locale = "English")
	{
		if (_textBase == null) {
			_textBase = new Dictionary<string,string> ();
		}
		
		TextAsset _localeString = Resources.Load<TextAsset> ("Locales/" + locale + "/text");
		
		if (_localeString == null) {
			Debug.LogWarning ("CAN'T FIND LOCALE '" + locale + "'. LOADING DEFAULT LOCALE '" + CurrentLocale + "'.");
			_localeString = Resources.Load<TextAsset> ("Locales/" + locale + "/text");
		}
		
		NanoXMLDocument document = new NanoXMLDocument (_localeString.text);
		NanoXMLNode RotNode = document.RootNode;
		
		foreach (NanoXMLNode node in RotNode.SubNodes) {
			if (node.Name.Equals ("String")) {
				_textBase.Add (node.GetAttribute ("id").Value, NormalizeDataString (node.Value));
			}
		}
	}
	
	public static string GetText (string id)
	{
		if (_textBase != null && _textBase.ContainsKey (id)) {
			return _textBase [id];
		} else {
			return "#" + id + "#";
		}
	}
	
	public static string GetSystemLanguage ()
	{
		return Application.systemLanguage.ToString ();
	}
	
	private static string NormalizeDataString (string ampersandTaggetString)
	{
		ampersandTaggetString = ampersandTaggetString.Replace ("&lt;", "<");
		ampersandTaggetString = ampersandTaggetString.Replace ("&gt;", ">");
		ampersandTaggetString = ampersandTaggetString.Replace ("&#13;", "\n");
		return ampersandTaggetString;
	}
}
