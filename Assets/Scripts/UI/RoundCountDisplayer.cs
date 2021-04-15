using TMPro;
using UnityEngine;

public class RoundCountDisplayer : MonoBehaviour
{
    private TMP_Text _text;
    private Vector3 _tempScale;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();        
    }

    public void RoundCountChange(int roundCounter)
    {
        _text.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        _text.text = Localer.GetText("Round") + " "  + (++roundCounter).ToString();
        LeanTween.value(gameObject, 0.2f, 1f, 0.7f).setOnUpdate((float val) => {
             _tempScale = new Vector3(val, val, 1f);
            _text.transform.localScale = _tempScale;
        });
    }

    public void ClearText()
    {
        _text.text = "";
    }
}
