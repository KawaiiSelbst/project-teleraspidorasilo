using UnityEngine;

[CreateAssetMenu(fileName = "New ItewData", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private Sprite icon;

    public string ItemName { get => itemName; }

    public Sprite Icon { get => icon; }
}
