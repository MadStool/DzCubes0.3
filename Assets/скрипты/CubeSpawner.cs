using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float DivisionOfCube = 2f;
    private const float ChanceReductionFactor = 2f;
    private const int LeftMouseButton = 0;

    [SerializeField] private int _minSpawnCube = 2;
    [SerializeField] private int _maxSpawnCube = 6;
    [SerializeField] private ExplodableCube _cubePrefab;
    [SerializeField] private CubeVisuals _cubeVisuals;
    [SerializeField] private CubePhysics _exploder;
    [SerializeField] private Camera _mainCamera;

    public ExplodableCube CubePrefab => _cubePrefab;

    private void Awake()
    {
        if (_cubeVisuals == null || _exploder == null || _mainCamera == null)
            Debug.LogError("Required dependencies are not assigned!");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            HandleCubeClick();
        }
    }

    private void HandleCubeClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) &&
            hit.collider.TryGetComponent(out ExplodableCube cube))
        {
            ProcessCubeClick(cube);
        }
    }

    public ExplodableCube[] SpawnChildCubes(Vector3 centerPosition, Vector3 parentScale, float parentSplitChance, Color parentColor)
    {
        if (_cubePrefab == null || _cubeVisuals == null) 
        {
            Debug.LogError("There are not enough links to spawn cubes");
            return null;
        }

        int childCount = Random.Range(_minSpawnCube, _maxSpawnCube + 1);
        ExplodableCube[] newCubes = new ExplodableCube[childCount];

        for(int i = 0; i < childCount; i++)
        {
            newCubes[i] = Instantiate(_cubePrefab, centerPosition, Random.rotation);
            newCubes[i].transform.localScale = parentScale / DivisionOfCube;

            Color variedColor = _cubeVisuals.GetVariedColor(parentColor);
            newCubes[i].Initialize(parentSplitChance / ChanceReductionFactor, variedColor);
        }

        return newCubes;
    }

    private void ProcessCubeClick(ExplodableCube cube)
    {
        var explosionCenter = cube.transform.position;

        if (cube.IsShouldSplit())
        {
            var newCubes = SpawnChildCubes(explosionCenter, cube.transform.localScale,
                                         cube.SplitChance, cube.CubeColor);

            _exploder.ExplodeCubes(newCubes, explosionCenter);
        }

        Destroy(cube.gameObject);
    }
}
