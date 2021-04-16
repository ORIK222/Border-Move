using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameWindow : MonoBehaviour {

    [SerializeField] TwoPlayerBorderController _border;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Button _buttonRestart;
    [SerializeField] Text _firstPlayerText;
    [SerializeField] Text _secondPlayerText;
    [SerializeField] GameObject _firstPlayerPanel;
    [SerializeField] GameObject _secondPlayerPanel;

    void Start () {
        _canvasGroup.alpha = 0f;
        EnableCanvasInteraction(false);
    }

    void EnableCanvasInteraction(bool state) {
        _canvasGroup.interactable = state;
        _canvasGroup.blocksRaycasts = state;
        _buttonRestart.interactable = state;        
    }

    public void Show(bool isSingle = false) {
        if (!isSingle)
        {
            if (_border.MyRectTransform.anchoredPosition.x < 0)
            {
                _firstPlayerText.text = Localer.GetText("Looser");
                _secondPlayerText.text = Localer.GetText("Winer");
                _firstPlayerPanel.SetActive(false);
                _secondPlayerPanel.SetActive(true);
            }
            else
            {
                _firstPlayerText.text = Localer.GetText("Winer");
                _secondPlayerText.text = Localer.GetText("Looser");
                _firstPlayerPanel.SetActive(true);
                _secondPlayerPanel.SetActive(false);
            }
        }
        else if (isSingle)
        {
            _firstPlayerText.text = Localer.GetText("Winer");
            _firstPlayerPanel.SetActive(true);
        }
            LeanTween.value(gameObject, _canvasGroup.alpha, 1f, 0.3f).setOnUpdate((float val) =>
            {
                _canvasGroup.alpha = val;
            }).setOnComplete(() =>
            {
                EnableCanvasInteraction(true);
            });
    }

    public void ButtonRestartOnClick() {
        GameManager.Instance.GameFlow.RestartGame();
    }
}
