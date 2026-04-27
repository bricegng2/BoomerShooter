using UnityEngine;
using UnityEngine.UIElements;

public class FPSCounter : MonoBehaviour
{
    VisualElement container;
    Label fpsText;
    private float pollingTime = 1.0f; // Update every 1 second
    private float time;
    private int frameCount;

    void Start()
    {
        container = GetComponent<UIDocument>().rootVisualElement;
        fpsText = container.Q<Label>("FPSText");

        // Set to 0 to disable VSync
        QualitySettings.vSyncCount = 0; 
        
        // Optionally set a target frame rate (e.g., -1 for uncapped)
        Application.targetFrameRate = -1; 
    }

    void Update()
    {
        time += Time.unscaledDeltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}