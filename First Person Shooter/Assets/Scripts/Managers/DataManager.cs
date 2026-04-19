using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;

    public static DataManager Instance { get { return _instance; } }
    public DestinationManager destinationManager { get; private set; }
    public InventoryManager inventoryManager { get; private set; }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
            destinationManager = gameObject.GetComponent<DestinationManager>();
            inventoryManager = gameObject.GetComponent<InventoryManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
