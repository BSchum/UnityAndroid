using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    [SerializeField] List<GameObject> elements;
    [SerializeField] Transform zoomedTransform;

    GameObject _currentElement = null; 
    Vector3 _currentElementLastPosition;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                OnElementTouched(raycastHit.collider.gameObject);
            }
        }
    }

    private void OnElementTouched(GameObject element)
    {
        Debug.Log($"{element.name} is touched!");
        if(_currentElement != null)
            _currentElement.transform.position = _currentElementLastPosition;

        _currentElement = element;
        _currentElementLastPosition = element.transform.position;
        element.transform.position = zoomedTransform.position;
    }
}

