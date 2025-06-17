using UnityEngine;
using UnityEngine.Rendering;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _explosionRadius = 3f;

    public void ExplodeCubes(ExplodableCube[] cubes, Vector3 explosionCenter)
    {
        foreach (var cube in cubes)
        {
            if (cube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius, 0, ForceMode.Impulse);
            }
        }
    }
}
