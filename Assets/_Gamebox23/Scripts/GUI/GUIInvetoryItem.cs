using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIInvetoryItem : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemAmountText;

    public Image ItemIcon => _itemIcon;

    public TextMeshProUGUI ItemAmountText => _itemAmountText;
}
