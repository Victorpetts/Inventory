using UnityEngine;
[CreateAssetMenu(fileName = "New equipment object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject {
    public float atkBonus;
    public float defenceBonus;

    public void Awake() {
        type = ItemType.Equipment;
    }
}
