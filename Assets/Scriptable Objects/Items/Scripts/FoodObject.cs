using UnityEngine;

[CreateAssetMenu(fileName = "New food object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject {
    public int restoreHealthVal;
    public void Awake() {
        type = ItemType.Food;
    }
}
