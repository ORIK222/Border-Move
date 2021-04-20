using System;

[Serializable]
public class Data
{
    public float ScoreLevelResult;
    public float SurvivalLevelTime;
    public string SurvivalLevelTimeInString;
    public int SingleLevelGameCount;
    public int CooperativeLevelGameCount;
    public int Language;
    public Data()
    {
        SurvivalLevelTimeInString = string.Empty;
        ScoreLevelResult = 0;
        SurvivalLevelTime = 0;
        SingleLevelGameCount = 0;
        CooperativeLevelGameCount = 0;
        Language = 1;
    }
}
