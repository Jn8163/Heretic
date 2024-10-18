using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public Pickup pickup;
}

public enum Pickup
{
    Vial,
    QuartzFlask,
    MysticUrn,
    Shield,
    EnchantedShield,
    Torch,
    MapScroll,
    BagOfHolding,
    ShadowSphere,
    TykettosTomeOfPower,
    ValadorsRingOfInvulnerability,
    InhiliconsWingsOfWrath,
    DarchalasChaosDevice,
    TorpolsMorphOvum,
    DelmintalitarsTimeBombOfTheAncients
}
