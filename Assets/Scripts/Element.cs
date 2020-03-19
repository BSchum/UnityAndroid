using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    bool _isSelected = false;
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
}
