using UnityEngine;

public class Ore : Interactable
{

    [SerializeField] private string m_oreName;
    [SerializeField] private int m_pickupRate;
    
    public override void Interact()
    {
        PlayerInventory.I.AddOrRemoveItem(m_oreName, m_pickupRate);
        Destroy(gameObject);
    }
}
