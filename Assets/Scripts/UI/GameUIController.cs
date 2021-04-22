using UnityEngine;

public class GameUIController : MonoBehaviour
{

    [SerializeField] private EndGameWindow _endGameWindow;
    [SerializeField] private GameObject _leftInfoPanel;
    [SerializeField] private GameObject _rightInfoPanel;
    [SerializeField] private GameObject _pausePanel;

    private void OnEnable()
    {
        EventManager.OnLostGameEvent += OnLostEvent;
    }

    private void OnDisable()
    {
        EventManager.OnLostGameEvent -= OnLostEvent;
    }

    private void OnLostEvent(EventData eData)
    {
        _leftInfoPanel.SetActive(false);
        if (!TypeGameSelector.IsSingle)
            _rightInfoPanel.SetActive(false);
        _endGameWindow.Show(TypeGameSelector.IsSingle, TypeGameSelector.IsScore);
    }

    public void PauseButtonOnClick()
    {
        _pausePanel.SetActive(!_pausePanel.activeSelf);
        var isPause = _pausePanel.activeSelf;
        if (isPause) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
}