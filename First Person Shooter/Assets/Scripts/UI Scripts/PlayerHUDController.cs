using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHUDController : MonoBehaviour
{
    public PlayerController player;

    VisualElement container;

    Label health;

    Label armour;

    Label ammo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        container = GetComponent<UIDocument>().rootVisualElement;
        health = container.Q<Label>("HealthNumber");
        armour = container.Q<Label>("ArmourNumber");
        ammo = container.Q<Label>("AmmoNumber");
        
        health.text = player.health.ToString();
        armour.text = player.armour.ToString();
        ammo.text = player.gun.GetAmmo().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int newHealth)
    {
        health.text = newHealth.ToString();
    }

    public void UpdateArmour(int newArmour)
    {
        armour.text = newArmour.ToString();
    }

    public void UpdateAmmo(int newAmmo)
    {
        ammo.text = newAmmo.ToString();
    }
}
