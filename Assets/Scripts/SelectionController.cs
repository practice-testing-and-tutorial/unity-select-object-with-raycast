using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private string _tagToCheck;

    [SerializeField]
    private Material _defaultMaterial;

    [SerializeField]
    private Material _selectedMaterial;

    private Material _currentMaterial;

    private Transform _lastSelectedTransform;
    
    private void Update()
    {
        var currentSelected = MouseSelection();

        _currentMaterial = currentSelected ? _selectedMaterial : _defaultMaterial;

        if (currentSelected)
        {
            _lastSelectedTransform = currentSelected;

            AssignMaterial(currentSelected, _currentMaterial);

            return;
        }

        AssignMaterial(_lastSelectedTransform, _currentMaterial);
    }

    private Transform MouseSelection()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag(_tagToCheck))
            {
                return raycastHit.transform;
            }
        }

        return null;
    }

    private void AssignMaterial(Transform target, Material material)
    {
        if (target)
        {
            var selectionRenderer = target.GetComponent<Renderer>();

            if (selectionRenderer && selectionRenderer.material != material)
            {
                selectionRenderer.material = material;
            }
        }
    }
}
