using UnityEngine;
using TMPro;

public class Multiplier : MonoBehaviour
{    
    public enum MultiplierType
    {
        Wrong = -1,
        Good = 0,
        Great = 1,
        Excellent = 2
    }

    public static int multiplierCount = 1;
    private MultiplierDisplayer _multiplierDisplayer;

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
    private void Awake()
    {
        _multiplierDisplayer = transform.GetChild(0).GetComponent<MultiplierDisplayer>();
    }


}
