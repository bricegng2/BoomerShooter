using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHUDController : MonoBehaviour
{
    public PlayerController player;

    VisualElement container;

    Label health;
    Label armour;
    Label ammo;

    VisualElement RedKey;
    VisualElement BlueKey;
    VisualElement YellowKey;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        container = GetComponent<UIDocument>().rootVisualElement;

        health = container.Q<Label>("HealthNumber");
        armour = container.Q<Label>("ArmourNumber");
        ammo = container.Q<Label>("AmmoNumber");

        RedKey = container.Q<VisualElement>("RedKey");
        BlueKey = container.Q<VisualElement>("BlueKey");
        YellowKey = container.Q<VisualElement>("YellowKey");

        UpdateKeys();

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

    public void UpdateKeys()
    {
        if (DataManager.Instance.inventoryManager.keys.Find(key => key.keyType == EKeyType.Red))
        {
            RedKey.style.backgroundColor = new Color(190.0f / 255.0f, 56.0f / 255.0f, 56.0f / 255.0f, 1.0f);
        }
        else
        {
            RedKey.style.backgroundColor = new Color(90.0f / 255.0f, 90.0f / 255.0f, 90.0f / 255.0f, 1.0f);
        }

        if (DataManager.Instance.inventoryManager.keys.Find(key => key.keyType == EKeyType.Blue))
        {
            BlueKey.style.backgroundColor = new Color(45.0f / 255.0f, 46.0f / 255.0f, 185.0f / 255.0f, 1.0f);
        }
        else
        {
            BlueKey.style.backgroundColor = new Color(90.0f / 255.0f, 90.0f / 255.0f, 90.0f / 255.0f, 1.0f);
        }

        if (DataManager.Instance.inventoryManager.keys.Find(key => key.keyType == EKeyType.Yellow))
        {
            YellowKey.style.backgroundColor = new Color(207.0f / 255.0f, 184.0f / 255.0f, 36.0f / 255.0f, 1.0f);
        }
        else
        {
            YellowKey.style.backgroundColor = new Color(90.0f / 255.0f, 90.0f / 255.0f, 90.0f / 255.0f, 1.0f);
        }
    }
}
