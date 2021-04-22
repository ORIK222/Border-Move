using UnityEngine;
using TMPro;

public class Multiplier : MonoBehaviour
{
    public static int multiplierCount = 1;
    private MultiplierDisplayer _multiplierDisplayer;

    private void Awake()
    {
        _multiplierDisplayer = transform.GetChild(0).GetComponent<MultiplierDisplayer>();
    }
    public void ChangeMultiplier(MultiplierType type)
    {

        if (type == MultiplierType.Wrong)
            multiplierCount = 1;
        else if (type != MultiplierType.Good)
        {
            if (multiplierCount + (int)type <= 10)
                multiplierCount += (int)type;
            else multiplierCount = 10;
        }
        _multiplierDisplayer.SetMultiplierText(multiplierCount);
    }
    public enum MultiplierType
    {
        Wrong = -1,
        Good = 0,
        Great = 1,
        Excellent = 2
    }
}
