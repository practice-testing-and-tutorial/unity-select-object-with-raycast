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

    private Transform _selectedTransform;
    
    private void Update()
    {
        if (IsSelecting())
        {
            Deselect();
            return;
        }

        MouseSelection();

        Select();
    }

    private void MouseSelection()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag(_tagToCheck))
            {
                _selectedTransform = raycastHit.transform;
            }
        }
    }

    private void Deselect()
    {
        if (_selectedTransform)
        {
            var selectionRenderer = _selectedTransform.GetComponent<Renderer>();

            if (selectionRenderer)
            {
                selectionRenderer.material = _defaultMaterial;

                _selectedTransform = null;
            }
        }
    }

    private void Select()
    {
        if (_selectedTransform)
        {
            var selectionRenderer = _selectedTransform.GetComponent<Renderer>();

            if (selectionRenderer)
            {
                selectionRenderer.material = _selectedMaterial;
            }
        }
    }

    private bool IsSelecting()
    {
        return _selectedTransform != null;
    }
}
