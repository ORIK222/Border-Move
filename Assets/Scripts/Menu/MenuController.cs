using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField] private Transform _settingPanel;
    [SerializeField] private Transform _resultPanel;

    public void StartButtonOnClick()
    {
        if (TypeGameSelector.IsSingle)
            GameManager.Instance.GameFlow.TransitToScene(UIConsts.SCENE_ID.SURVIVALLEVEL);
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
}