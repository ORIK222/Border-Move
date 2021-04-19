using UnityEngine;

public class LoosePlace : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var border = collision.gameObject.GetComponent<SurvivalBorder>();
        if (border)
        {
            SurvivalLevel.EndGame();
        }
    }
}
