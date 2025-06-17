using UnityEngine;

public class CubeColorManager : MonoBehaviour
{
    [Header("настройки цвета/интенсивность")]
    [SerializeField] private Color[] _availableColors;
    [SerializeField] private float _colorVariation = 10f;

    public void ApplyRandomColor(GameObject cube)
    {
        if(cube.TryGetComponent<Renderer>(out var renderer))
        {
            Color randomColor = GetRandomColor();
            renderer.material.color = randomColor;
        }
    }

    public void ApplyColorVariation(GameObject cube, Color baseColor)
    {
        if(cube.TryGetComponent<Renderer>(out var renderer))
        {
            Color variedColor = GetVariedColor(baseColor);
            renderer.material.color = variedColor;
        }
    }

    private Color GetRandomColor()
    {
        Color[] simpleColors = new Color[]
         {
             Color.red,
             Color.green,
             Color.blue,
             Color.yellow,
             Color.magenta,
             Color.cyan
          };

        return simpleColors[Random.Range(0, simpleColors.Length)];
    }

    public Color GetVariedColor(Color baseColor)
    {
        return new Color(
            GetVariedChannel(baseColor.r),
            GetVariedChannel(baseColor.g),
            GetVariedChannel(baseColor.b),
            1f
        );
    }

    private float GetVariedChannel(float channel)
    {
        return Mathf.Clamp01(channel + Random.Range(-_colorVariation, _colorVariation));
    }
}