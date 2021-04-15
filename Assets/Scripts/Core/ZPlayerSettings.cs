/*! 
 * PlayerSettings: Class used to save/load player data into Unity's PlayerPrefs * 
 */
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class UserData
{
    public string Name;
    public int AvatarId;
    public int CurrentLevelNumber;
    public int LevelsCompleted;

	// options
    public bool Alarms;
    public float MusicVolume;
    public float SoundVolume;

	// music
    public string LastTrack;

	//
    public bool DontShowExitGameWindow;

	public UserData ()
	{
	}

	public void SetDefaults ()
	{
		CurrentLevelNumber = 1;
		LevelsCompleted = 0;
		AvatarId = 0;
		MusicVolume = 0.65f;
		SoundVolume = 0.65f;
		DontShowExitGameWindow = false;
	}
}

public sealed class ZPlayerSettings
{
    [System.Serializable]
    public class DictUsers : SerializableDictionary<string, UserData> {}


	public static int SettingsVersion = 1;
	public UserData User;
    public DictUsers Users;

	public ZPlayerSettings ()
	{
		User = null;
        Users = new DictUsers();
	}

    /// <summary>
    /// Loads user data from Unity's PlayerPrefs
    /// </summary>
    public void Load()
    {
        Debug.Log("SETTINGS: Start Loading");

        if (PlayerPrefs.HasKey(Consts.APP_SHORTNAME))
        {            
            Debug.Log("SETTINGS: Loading from local device...");
            string rawStringBinaryData = PlayerPrefs.GetString(Consts.APP_SHORTNAME);
            StandaloneSavedData standaloneSavedData = null;
            try
            {
                string decompressedJSON = GZipHelper.Decompress(rawStringBinaryData);
                standaloneSavedData = JsonUtility.FromJson<StandaloneSavedData>(decompressedJSON);
            }
            catch (Exception ex)
            {
                Debug.Log("SETTING: Error parsing local saved settings object.\n" + ex.Message);
            }                
            LoadSettingsFromSavedData(standaloneSavedData);
        }
        else
        {
            Debug.Log("SETTINGS: No saved settings found. Creating new settings.");
            ResetSettings();            
            Save();
            Load();            
        }        
    }

    public void LoadSettingsFromSavedData(StandaloneSavedData sData)
    {
        if (sData == null)
        {
            // new game data
            ResetSettings();
            Debug.Log("SETTINGS: Error loading StandaloneSavedData, creating new settings.");
        }
        else
        {
            SettingsVersion = sData.SettingsVersion;
            Users = sData.Users;
            ChangeUser(sData.CurrentUser);

            User.Name = sData.CurrentUser;
            User.AvatarId = sData.AvatarId; 
            User.CurrentLevelNumber = sData.CurrentLevelNumber;
            User.LevelsCompleted = sData.LevelsCompleted;

            // options
            User.MusicVolume = sData.MusicVolume;
            User.SoundVolume = sData.SoundVolume;
            User.LastTrack = sData.LastTrack;
            User.DontShowExitGameWindow = sData.DontShowExitGameWindow;
            User.Alarms = sData.Alarms;

            Debug.Log("SETTINGS: Loaded successfully.");
        }
        sData = null;        
    }

    /// <summary>
    /// Saves user data to Unity's PlayerPrefs
    /// </summary>
    public void Save(bool isExitSave = false)
    {       
        StandaloneSavedData sData = new StandaloneSavedData();

        sData.SettingsVersion = SettingsVersion;
        sData.Users = Users;
        sData.CurrentUser = User.Name;
        sData.AvatarId = User.AvatarId;
        sData.CurrentLevelNumber = User.CurrentLevelNumber;
        sData.LevelsCompleted = User.LevelsCompleted;

        // options
        sData.MusicVolume = User.MusicVolume;
        sData.SoundVolume = User.SoundVolume;
        sData.LastTrack = User.LastTrack;
        sData.DontShowExitGameWindow = User.DontShowExitGameWindow;
        sData.Alarms = User.Alarms;

        string json = GZipHelper.Compress(JsonUtility.ToJson (sData));
        PlayerPrefs.SetString(Consts.APP_SHORTNAME, json);
        Debug.Log("SETTINGS: Saving to local device.");
        PlayerPrefs.SetInt("SettingsVersion", SettingsVersion);
        PlayerPrefs.Save();
    }

	/// <summary>
	/// Clear all the settings and set them to default values
	/// </summary>
	public void ResetSettings ()
	{
		PlayerPrefs.DeleteAll ();
		Users.Clear ();
		AddUser ("default");
		ChangeUser ("default");
        User.SetDefaults();
		Save ();
	}

	public void AddUser (string name)
	{
		foreach (KeyValuePair<string, UserData> entry in Users) {
			if (entry.Value.Name == name) {
				Debug.Log ("User " + name + "Exists!");
				return;
			}
		}

		UserData user = new UserData ();
		user.Name = name;
		user.SetDefaults ();
		Users [name] = user;
	}

	public void RemoveUser (string name)
	{
		Users.Remove (name);
	}

	public void ChangeUser (string name)
	{
		User = Users [name];
		SetMusicVolume (User.MusicVolume);
		SetSoundVolume (User.SoundVolume);
	}

	public void SetMusicVolume (float fVolume)
	{
		User.MusicVolume = fVolume;
		//MusicManager.setMusicVolume (fVolume);
	}

	public void SetSoundVolume (float fVolume)
	{
		User.SoundVolume = fVolume;
		//MusicManager.setSoundVolume (fVolume);
	}
}