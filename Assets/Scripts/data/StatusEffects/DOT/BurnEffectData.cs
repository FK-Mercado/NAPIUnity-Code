using UnityEngine;

namespace NAPI.Data
{
    [CreateAssetMenu(
        fileName = "BurnEffect",
        menuName = "NAPI/Status Effects/DOT/Quemadura (Fuego)")]
    public class BurnEffectData : DamageOverTimeEffectData
    {
        // Sin nada acá: toda la lógica vive en DamageOverTimeEffectData.
        // Esta clase existe solo para que "Quemadura" sea su propia
        // entrada en el menú Create, con sus propios valores por asset.
    }
}
