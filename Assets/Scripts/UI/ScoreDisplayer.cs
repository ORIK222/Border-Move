using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private SinglePlayer _player;
    private TMP_Text _text;

    private void OnEnable()
    {
        _player.RoundWinned += OnRoundWinned;
    }
    private void OnDisable()
    {
        _player.RoundWinned -= OnRoundWinned;
    }
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = "Score: 0";
    }

    private void OnRoundWinned(int warriorCount)
    {
        _text.text = "Score: " + warriorCount.ToString();
    }
}
