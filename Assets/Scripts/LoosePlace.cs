using UnityEngine;

public class LoosePlace : MonoBehaviour
{
    [SerializeField] SurvivalLevel _survivalLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var border = collision.gameObject.GetComponent<SurvivalBorder>();
        if (border)
        {
            _survivalLevel.EndGame();
        }
    }
}
