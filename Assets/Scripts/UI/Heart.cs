using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public static bool isEmpty;

    [SerializeField] private ScoreLevel _scoreLevel;
    private Image[] _hearts;
    private int _number;

    private void OnEnable()
    {
        _scoreLevel.OnRoundLoseEvent += Decrease;
    }
    private void OnDisable()
    {
        _scoreLevel.OnRoundLoseEvent -= Decrease;
    }
    private void Start()
    {
        _number = 0;
        _hearts = new Image[transform.childCount];
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }
    private void Decrease()
    {
        Destroy(_hearts[_number]);
        _number++;
        if (_number == _hearts.Length)
        {
            isEmpty = true;
        }
    }
}
