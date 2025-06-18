using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Exploder), typeof(CubeVisuals))]
public class CubeSpawner : MonoBehaviour
{
    private const float DivisionOfCube = 2f;
    private const float ChanceReductionFactor = 2f;

    [SerializeField] private int minSpawnCount = 2;
    [SerializeField] private int maxSpawnCount = 6;
    [SerializeField] private ExplodableCube cubePrefab;

    private Exploder _exploder;
    private CubeVisuals _cubeVisuals;

    public ExplodableCube CubePrefab => cubePrefab;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _cubeVisuals = GetComponent<CubeVisuals>();
    }

    public ExplodableCube[] SpawnChildCubes(Vector3 center, Vector3 parentScale, float parentSplitChance, Color parentColor)
    {
        if (cubePrefab == null) return null;

        int count = Random.Range(minSpawnCount, maxSpawnCount + 1);
        var newCubes = new ExplodableCube[count];

        for (int i = 0; i < count; i++)
        {
            newCubes[i] = Instantiate(cubePrefab, center, Random.rotation);
            newCubes[i].transform.localScale = parentScale / DivisionOfCube;
            newCubes[i].Initialize(
                parentSplitChance / ChanceReductionFactor,
                _cubeVisuals.GetVariedColor(parentColor)
            );
        }

        return newCubes;
    }

    public void ProcessCubeClick(ExplodableCube cube)
    {
        var explosionCenter = cube.transform.position;
        var nearbyCubes = Physics.OverlapSphere(explosionCenter, cube.ExplosionRadius)
            .Select(cubes => cubes.GetComponent<ExplodableCube>())
            .Where(cubes => cubes != null)
            .ToArray();

        _exploder.ExplodeCubes(nearbyCubes, explosionCenter);

        if (cube.ShouldSplit())
        {
            var newCubes = SpawnChildCubes(explosionCenter, cube.transform.localScale,
                cube.SplitChance, cube.CubeColor);
            _exploder.ExplodeCubes(newCubes, explosionCenter);
        }

        Destroy(cube.gameObject);
    }
}