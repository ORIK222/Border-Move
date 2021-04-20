using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingLocaler : MonoBehaviour
{
    [SerializeField] private TMP_Text _settingTitleText;
    [SerializeField] private TMP_Text _languageTitleText;
    [SerializeField] private TMP_Text _volumeTitleText;
    [SerializeField] private TMP_Text _soundTitleText;
    [SerializeField] private TMP_Text _musicTitleText;
    [SerializeField] private Result _result;
    private int _currentLanguages = 0;

    [SerializeField] private List<Sprite> _localeButtonSprites;
    [SerializeField] private Button _localerButton;
    private void Start()
    {
        _currentLanguages = GameManager.Instance.data.Language;

        Localer.CurrentLocale = Localer.AllLanguages[_currentLanguages];
        Localer.Init(Localer.CurrentLocale);
        _localerButton.image.sprite = _localeButtonSprites[_currentLanguages];
        ChangeText();
        _result.SetResultData();
    }

    public void LocalerOnButtonClick()
    {
        if (_currentLanguages + 1 < Localer.AllLanguages.Count)
            _currentLanguages++;
        else _currentLanguages = 0;
        _localerButton.image.sprite = _localeButtonSprites[_currentLanguages];
        Localer.CurrentLocale = Localer.AllLanguages[_currentLanguages];
        Localer.Init(Localer.CurrentLocale);
        GameManager.Instance.data.Language = _currentLanguages;
        ChangeText();
        _result.SetResultData();
    }

    public void ChangeText()
    {
        _settingTitleText.text = Localer.GetText("Setting");
        _languageTitleText.text = Localer.GetText("Language");
        _volumeTitleText.text = Localer.GetText("Volume");
        _soundTitleText.text = Localer.GetText("Sound");
        _musicTitleText.text = Localer.GetText("Music");
    }
}
