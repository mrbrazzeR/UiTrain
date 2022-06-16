using TMPro;
using UiTrain.CharacterStats;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatDisplay : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private CharacterStat _stat;

    public CharacterStat Stat
    {
        get { return _stat;}
        set
        {
            _stat = value;
           UpdateValue();
        }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            NameText.text = _name;
        }
    }

    public void UpdateValue()
    {
        ValueText.text = _stat.Value.ToString();
    }
    
    
    
    public TMP_Text NameText;
    public TMP_Text ValueText;

    [SerializeField] StatTooltip tooltip;

    void OnValidate()
    {
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        NameText = texts[0];
        ValueText = texts[1];

        if (tooltip == null)
        {
            tooltip = FindObjectOfType<StatTooltip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       tooltip.ShowTooltip(Stat,Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       tooltip.HideToolTip();
    }
}
