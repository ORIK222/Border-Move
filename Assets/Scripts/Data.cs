using System;

[Serializable]
public class Data
{
    private float _scoreLevelResult;
    private float _survivalLevelTime;
    private int _singleLevelGameCount;
    private int _cooperativeLevelGameCount;
    private string _language;
    Data()
    {
        _scoreLevelResult = 0;
        _survivalLevelTime = 0;
        _singleLevelGameCount = 0;
        _cooperativeLevelGameCount = 0;
        _language = "English";
    }
}
