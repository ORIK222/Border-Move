using TMPro;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Transform _soundSettingPanel;

    public void SoundButtonOnClick()
    {
        _soundSettingPanel.gameObject.SetActive(!_soundSettingPanel.gameObject.activeSelf);
    }
}