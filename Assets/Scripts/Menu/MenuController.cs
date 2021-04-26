using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private SettingLocaler _settingLocaler;
    [SerializeField] private Transform _typeSelectPanel;
    [SerializeField] private Transform _settingPanel;
    [SerializeField] private Transform _resultPanel;

    private void Start()
    {
        _settingLocaler.LocalerOnButtonClick();
    }

    public void StartButtonOnClick()
    {
        if (TypeGameSelector.IsSingle)
            _typeSelectPanel.gameObject.SetActive(true);
        else
            GameManager.Instance.GameFlow.TransitToScene(UIConsts.SCENE_ID.TWOPLAYERLEVEL);
    }
    public void SettingButtonOnClick()
    {
        _settingPanel.gameObject.SetActive(!_settingPanel.gameObject.activeSelf);
    }
    public void ResultButtonOnClick()
    {
        _resultPanel.gameObject.SetActive(!_resultPanel.gameObject.activeSelf);
    }
    public void SurvivalButtonOnClick()
    {
        TypeGameSelector.IsScore = false;
        GameManager.Instance.GameFlow.TransitToScene(UIConsts.SCENE_ID.SURVIVALLEVEL);
    }
    public void ScoreButtonOnClick()
    {
        TypeGameSelector.IsScore = true;
        GameManager.Instance.GameFlow.TransitToScene(UIConsts.SCENE_ID.SCORELEVEL);
    }
}