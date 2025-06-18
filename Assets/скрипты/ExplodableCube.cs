using UnityEngine;

public class ExplodableCube : MonoBehaviour
{
    [SerializeField] private float splitChance = 1f;
    private Color _cubeColor;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    public float SplitChance => splitChance;
    public Color CubeColor => _cubeColor;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        TryGetComponent(out _renderer);
        TryGetComponent(out _rigidbody);

        if (_renderer == null)
            Debug.LogError("Renderer not found in Cube!");
    }

    public void Initialize(float chance, Color color)
    {
        splitChance = chance;
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