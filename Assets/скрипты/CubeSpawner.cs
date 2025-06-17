using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float DivisionOfCube = 2f;
    private float _chanceReductionFactor = 2f;

    [SerializeField] private ExplodableCube _cubePrefab;
    [SerializeField] private int _minSpawnCube = 2;
    [SerializeField] private int _maxSpawnCube = 6;
    [SerializeField] private CubeColorManager _colorManager;

    private void Awake()
    {
        if (_colorManager == null) 
        {
            _colorManager = FindAnyObjectByType<CubeColorManager>();

            if (_colorManager == null) 
                Debug.LogError("CubeColorManager не найден!");
        }
    }

    public ExplodableCube[] SpawnChildCubes(Vector3 centerPosition, Vector3 parentScale, float parentSplitChance, Color parentColor)
    {
        if (_cubePrefab == null || _colorManager == null) 
        {
            Debug.LogWarning("Ќе хватает ссылок дл€ спавна кубов");
            return null;
        }

        int childCount = Random.Range(_minSpawnCube, _maxSpawnCube + 1);
        ExplodableCube[] newCubes = new ExplodableCube[childCount];

        for(int i = 0; i < childCount; i++)
        {
            newCubes[i] = Instantiate(_cubePrefab, centerPosition, Random.rotation);
            newCubes[i].transform.localScale = parentScale / DivisionOfCube;
            newCubes[i].Initialize(parentSplitChance / _chanceReductionFactor);
            _colorManager.ApplyColorVariation(newCubes[i].gameObject, parentColor);
        }

        return newCubes;
    }
}
