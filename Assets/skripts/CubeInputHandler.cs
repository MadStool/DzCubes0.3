using UnityEngine;

public class CubeInputHandler : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CubeInteractionHandler _interactionHandler;

    private void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
            HandleClick();
    }

    private void HandleClick()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent(out ExplodableCube cube))
        {
            _interactionHandler.HandleCubeClick(cube);
        }
    }
}