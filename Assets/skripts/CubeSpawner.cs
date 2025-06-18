using UnityEngine;

[RequireComponent(typeof(CubeVisuals))]
public class CubeSpawner : MonoBehaviour
{
    private const float DivisionOfCube = 2f;
    private const float ChanceReductionFactor = 2f;

    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private ExplodableCube _cubePrefab;

    private CubeVisuals _cubeVisuals;

    public ExplodableCube CubePrefab => _cubePrefab;

    private void Awake()
    {
        _cubeVisuals = GetComponent<CubeVisuals>();
    }

    public ExplodableCube[] SpawnChildCubes(Vector3 center, Vector3 parentScale, float parentSplitChance, Color parentColor)
    {
        if (_cubePrefab == null) 
            return null;

        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        var newCubes = new ExplodableCube[count];

        for (int i = 0; i < count; i++)
        {
            newCubes[i] = Instantiate(_cubePrefab, center, Random.rotation);
            newCubes[i].transform.localScale = parentScale / DivisionOfCube;
            newCubes[i].Initialize(parentSplitChance / ChanceReductionFactor, _cubeVisuals.GetVariedColor(parentColor));
        }

        return newCubes;
    }
}