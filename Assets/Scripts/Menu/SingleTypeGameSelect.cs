using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleTypeGameSelect : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;

    private void Start()
    {
        _titleText.text = Localer.GetText("SelectType");
    }
    public void CloseButtonOnClick()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
