using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CubeExploder _exploder;
    [SerializeField] private CubeSpawner _spawner;

    private void Awake()
    {
        if (_exploder == null) 
            Debug.LogError("CubeExploder not assigned in GameManager!");

        if (_spawner == null) 
            Debug.LogError("CubeSpawner not assigned in GameManager!");
    }

    public void HandleCubeClick(ExplodableCube cube)
    {
        if (_exploder == null || _spawner == null || cube == null)
        {
            return;
        }

        bool shouldSplit = cube.IsShouldSplit();
        Vector3 explosionCenter = cube.transform.position;

        if (shouldSplit)
        {
            ExplodableCube[] newCubes = _spawner.SpawnChildCubes(
                explosionCenter,
                cube.transform.localScale,
                cube.SplitChance,
                cube.CubeColor
            );

            if (newCubes != null && newCubes.Length > 0)
            {
                _exploder.ExplodeCubes(newCubes, explosionCenter);
            }
        }

        Destroy(cube.gameObject);
    }
}
