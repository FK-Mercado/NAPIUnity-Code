using UnityEngine;

namespace NAPI.Data
{
    [CreateAssetMenu(fileName = "Item_", menuName = "NAPI/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Identidad")]
        public string id;
        public string itemName;
        [TextArea] public string description;
        public Sprite icon;

        [Header("Clasificación (punto 6 del GDD: los 4 objetos)")]
        public ItemType itemType;
        public TargetType targetType;

        [Header("Efecto")]
        [Tooltip("Cantidad de HP/EN curados, daño infligido, o magnitud del buff/debuff, según itemType")]
        public int effectAmount;
        [Tooltip("Duración en turnos, solo aplica a buffs/debuffs. 0 = instantáneo (cura/daño)")]
        public int durationInTurns;

        [Header("Tienda")]
        public int shopCost;
        [Tooltip("Si es false, no aparece en la tienda (por ejemplo, ítems de misión)")]
        public bool purchasableInShop = true;
    }
}
