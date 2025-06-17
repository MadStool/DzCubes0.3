using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        if(_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleCubeClick();
        }
    }

    private void HandleCubeClick()
    {
        if(_gameManager == null)
        {
            Debug.LogError("GameManager not assigned!");
            return;
        }

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out ExplodableCube cube))
        {
            _gameManager.HandleCubeClick(cube);
        }
    }
}
