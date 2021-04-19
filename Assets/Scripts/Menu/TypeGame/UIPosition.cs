using UnityEngine.UI;
using UnityEngine;

public class UIPosition : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    private Vector3[] _positions;
    private Quaternion[] _rotates;

    private void Awake()
    {
        var buttons = FindObjectsOfType<Button>();
        _buttons = new Button[buttons.Length];
        _positions = new Vector3[_buttons.Length];
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i] = buttons[i];
        }
    }

    public void ChangePosition()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _positions[i] = _buttons[i].transform.position;
            // _rotates[i] = _buttons[i].transform.rotation;
            Debug.Log(_positions[i]);
        }                
    }
    public void SetPosition()
    {
        foreach (var position in _positions)
        {
            Debug.Log(position);
        }
        if (_positions[3] != Vector3.zero)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                Debug.Log(_positions[i]);
                _buttons[i].transform.position = _positions[i];
                _buttons[i].transform.rotation = _rotates[i];
            }
        }
    }
}
