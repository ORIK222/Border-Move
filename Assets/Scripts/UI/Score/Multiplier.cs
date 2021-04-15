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
        
       if(type == MultiplierType.Wrong)
            multiplierCount = 1;
       else if (type != MultiplierType.Good)
            multiplierCount += (int)type;
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
