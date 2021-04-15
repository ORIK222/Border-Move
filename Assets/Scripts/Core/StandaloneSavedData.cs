[System.Serializable]
public class StandaloneSavedData
{
    public int SettingsVersion;
    public ZPlayerSettings.DictUsers Users;

    public string CurrentUser;
    public int AvatarId;
    public int CurrentLevelNumber;
    public int LevelsCompleted;

    // options
    public float MusicVolume;
    public float SoundVolume;
    public string LastTrack;
    public bool DontShowExitGameWindow;
    public bool Alarms;

    public StandaloneSavedData() 
    {

    }
}
