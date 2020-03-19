using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private MaterialsProvider provider;
    [SerializeField] private Renderer renderer;

    bool _isSelected = false;
    int _materialIndex = 0;
    // Update is called once per frame
    void Update()
    {
        if(!_isSelected)
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime));
    }

    public void OnSelect()
    {
        _isSelected = true;
    }

    public void OnDeselect()
    {
        _isSelected = false;
    }

    public void ScrollMaterials()
    {
        renderer.material = provider.GetMaterial(_materialIndex);
        _materialIndex++;
        if(_materialIndex >= provider.MaterialCount)
        {
            _materialIndex = 0;
        }
    }
}
