using UnityEngine;

[RequireComponent(typeof(Renderer)), RequireComponent(typeof(Rigidbody))]
public class ExplodableCube : MonoBehaviour
{
    [SerializeField] private float splitChance = 1f;
    [SerializeField] private float explosionRadiusMultiplier = 2f;
    [SerializeField] private float explosionForceMultiplier = 3f;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Color _cubeColor;

    public float SplitChance => splitChance;
    public Color CubeColor => _cubeColor;
    public Rigidbody CubeRigidbody => _rigidbody;
    public float ExplosionRadius => transform.localScale.x * explosionRadiusMultiplier;
    public float ExplosionForce => explosionForceMultiplier / transform.localScale.x;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(float chance, Color color)
    {
        splitChance = chance;
        _cubeColor = color;
        _renderer.material.color = color;
    }

    public bool ShouldSplit() => Random.value <= splitChance;
}