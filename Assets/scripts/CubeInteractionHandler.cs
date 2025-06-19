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
}