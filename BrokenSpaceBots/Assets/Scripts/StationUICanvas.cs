using UnityEngine;
using UnityEngine.UI;

public class StationUICanvas : MonoBehaviour
{
    public Text stationName;
    public Image uiBackground;
    public ProgressBar progressBar;

    public string StationName { set { stationName.text = value; } }

    public Color StationColor { set { uiBackground.color = value; } }
    public void SetHealth(float current, float maximum)
    {
        progressBar.current = current;
        progressBar.maximum = maximum;
    }

}
