using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    public static int Score;

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

    private void OnRoundWinned(int score)
    {
        Score = score;
        _text.text = "Score: " + score.ToString();
    }
}
