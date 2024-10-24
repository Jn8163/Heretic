using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Pickup pickup;
    public Sprite image;
    [HideInInspector] public Item item;
    [SerializeField] private AudioSource audioSource;
    protected InventorySystem inventorySystem;



    protected virtual void Start()
    {
        item = this;
        inventorySystem = InventorySystem.instance;
    }



    protected virtual void PickupItem()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}



public enum Pickup
{
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