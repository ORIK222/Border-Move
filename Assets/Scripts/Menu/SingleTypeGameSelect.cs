using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTypeGameSelect : MonoBehaviour
{
    public void CloseButtonOnClick()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
