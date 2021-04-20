using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] private TMP_Text _singleLevelCountText;
    [SerializeField] private TMP_Text _duoLevelCountText;
    [SerializeField] private TMP_Text _bestTimeText;
    [SerializeField] private TMP_Text _bestScoreText;

    private void Start()
    {
        SetResultData();
    }
    public void SetResultData()
    {
        _singleLevelCountText.text = Localer.GetText("SingleCount") + ": " + GameManager.Instance.data.SingleLevelGameCount.ToString();
        _duoLevelCountText.text = Localer.GetText("DuoCount") + ": " + GameManager.Instance.data.CooperativeLevelGameCount.ToString();
        _bestTimeText.text = Localer.GetText("BestTime") + ": " + GameManager.Instance.data.SurvivalLevelTimeInString;
        _bestScoreText.text = Localer.GetText("BestScore") + ": " + GameManager.Instance.data.ScoreLevelResult.ToString();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
