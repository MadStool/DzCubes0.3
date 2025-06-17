using UnityEngine;

public class ExplodableCube : MonoBehaviour
{
    [SerializeField] private float splitChance = 1f;
    private Color _cubeColor;
    private Renderer _renderer;
    public float SplitChance => splitChance;
    public Color CubeColor => _cubeColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        if (_renderer == null) 
            Debug.LogError("Renderer not found in Cuba!");
    }

    public void Initialize(float chance)
    {
        splitChance = chance;
    }

    public void SetColor(Color color)
    {
        _cubeColor = color;

        if (_renderer != null)
        {
            _renderer.material.color = color;
        }
    }

    public bool IsShouldSplit()
    {
        return Random.value <= splitChance;
    }
}