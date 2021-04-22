using UnityEngine;
using TMPro;

public class MultiplierDisplayer : MonoBehaviour
{
    private TMP_Text _text;    

    public void SetMultiplierText(int multiplier)
    {
        _text.text = "x" + multiplier.ToString();
    }

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }



}
