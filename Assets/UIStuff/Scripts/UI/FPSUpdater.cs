using UnityEngine;
using TMPro;

public class FPSUpdater : MonoBehaviour
{
    [SerializeField] private float updateFrequency = 0.2f;

    private float fps;
    private TextMeshProUGUI fpsText;
    private float updateTimer;

    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        updateTimer = updateFrequency;
    }

    private void Update()
    {
        UpdateFPSDisplay();
    }

    private void UpdateFPSDisplay()
    {
        updateTimer -= Time.deltaTime;

        if(updateTimer <= 0)
        {
            fps = 1 / Time.unscaledDeltaTime;
            fpsText.text = "FPS: " + Mathf.Round(fps);
            updateTimer = updateFrequency;
        }
    }
}
