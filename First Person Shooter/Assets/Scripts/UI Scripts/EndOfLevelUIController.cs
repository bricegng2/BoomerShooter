using UnityEngine;
using UnityEngine.UIElements;

public class EndOfLevelUIController : MonoBehaviour
{
    VisualElement container;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        container = GetComponent<UIDocument>().rootVisualElement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
