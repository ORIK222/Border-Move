using UnityEngine;
using TMPro;

public class MultiplierDisplayer : MonoBehaviour
{
    private TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetMultiplierText(int multiplier)
    {
        _text.text = "x" + multiplier.ToString();
    }

}
