using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerTaskDisplayer : MonoBehaviour {


    [SerializeField] private Text _infoPanel;
    [SerializeField] private Image _playerImage;


    public void AchivePositiveEffect()
    {
//        Debug.Log("Win round " + name);
        _playerImage.color = Color.green;
    }
    public void AchiveNegativeEffect(){
        _playerImage.color = Color.red;
    }
    public void PassiveEffect() {
        _playerImage.color = Color.yellow;
    }
    private void ResetEffect() {
        _playerImage.color = Color.white;
    }

    public void StartRound() {
        ResetEffect();
    }

    public void UpdateInfoPanel(List<CommandsManager.CommandType> commands, int variation) {
        string str = "";

        foreach (var item in commands)
        {
            str += Localer.GetText("CMD." + item.ToString() + "_" + variation) + "\n";
        }
        ShowText(str);
    }

    void ShowText(string str) {
        _infoPanel.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
		_infoPanel.text = str;
        LeanTween.value(gameObject, 0.2f, 1f, 0.15f).setOnUpdate((float val) => {
            _infoPanel.transform.localScale = new Vector3(val, val, 1f);
        });
    }

    public void ClearInfoPanel() {
        _infoPanel.text = "";
    }
}
