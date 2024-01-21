using UnityEngine;
using UnityEngine.UI;

public class UiTextController : MonoBehaviour
{
    [SerializeField] protected Text[] targetExitText;
    [SerializeField] protected string firstTextString;
    [SerializeField] protected string lastTextString;
    protected string Text
    {
        set
        {
            foreach (var tText in targetExitText)
                tText.text = $"{firstTextString}{value}{lastTextString}";
        }
    }
    public virtual void OnChangeTargetValueAmount(string value)
    {
        Text = value;
    }
    public virtual void OnChangeTargetValueAmount(int iValue)
    {
        OnChangeTargetValueAmount(iValue.ToString());
    }
    public virtual void OnChangeTargetValueAmount(float fValue)
    {
        OnChangeTargetValueAmount(fValue.ToString());
    }
}