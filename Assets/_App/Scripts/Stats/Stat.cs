using UnityEngine;
[System.Serializable]
public class Stat
{
    [SerializeField]
    private int maxValue, currentValue;
    
    [SerializeField]
    private object source;

    public int MaxValue
    {
        get { return maxValue; }
        set 
        { 
            maxValue = value;
            currentValue = maxValue;
        }
    }
    public int CurrentValue
    {
        get { return currentValue; }
        set 
        {
            if(value <= 0)
            {
                currentValue = 0;
            }
            else if (value >= maxValue)
            {
                currentValue = maxValue;
            }
            else
            {
                currentValue = value;
            }
        }
    }
}
