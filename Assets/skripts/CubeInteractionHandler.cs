using UnityEngine;
using System.Linq;

public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private Exploder _exploder;

    public void HandleCubeClick(ExplodableCube cube)
    {
        if (cube == null) 
            return;

        Vector3 explosionCenter = cube.transform.position;

        ExplodeNearbyCubes(cube, explosionCenter);

        if (cube.ShouldSplit())
        {
            var newCubes = _spawner.SpawnChildCubes(
                explosionCenter,
                cube.transform.localScale,
                cube.SplitChance,
                cube.CubeColor
            );
            _exploder.ExplodeCubes(newCubes, explosionCenter);
        }

        Destroy(cube.gameObject);
    }

    private void ExplodeNearbyCubes(ExplodableCube cube, Vector3 explosionCenter)
    {
        var nearbyCubes = Physics.OverlapSphere(explosionCenter, cube.ExplosionRadius)
            .Select(colllider => colllider.GetComponent<ExplodableCube>())
            .Where(cubes => cubes != null)
            .ToArray();

        _exploder.ExplodeCubes(nearbyCubes, explosionCenter);
    }
}