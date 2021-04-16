using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField] private Transform _settingPanel;

    private void Start()
    {
        _settingPanel.gameObject.SetActive(false);
    }
    public void StartButtonOnClick()
    {
        if (TypeGameSelector.IsSingle)
            GameManager.Instance.GameFlow.TransitToScene(UIConsts.SCENE_ID.SCORELEVEL);
        else
            GameManager.Instance.GameFlow.TransitToScene(UIConsts.SCENE_ID.TWOPLAYERLEVEL);
    }
    public void SettingButtonOnClick()
    {
        _settingPanel.gameObject.SetActive(!_settingPanel.gameObject.activeSelf);
    }
}