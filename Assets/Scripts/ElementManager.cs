using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    [SerializeField] Transform zoomedTransform;
    [SerializeField] Transform elementHolder;
    [SerializeField] float _touchRotationSpeed;

    Element[] _elements;

    Element _currentElement = null;
    Vector3 _currentElementLastPosition;



    private void Start()
    {
        _elements = elementHolder.GetComponentsInChildren<Element>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Ray raycast = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit raycastHit;

            switch (touch.phase)
            {
                case TouchPhase.Ended:
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        if (raycastHit.collider.CompareTag("Element"))
                            if (_currentElement == null)
                                OnElementTouched(raycastHit.collider.gameObject.GetComponent<Element>());
                            else
                                _currentElement.ScrollMaterials();
                    }
                    
                    break;
                case TouchPhase.Moved:
                    if (_currentElement != null)
                        _currentElement.transform.Rotate(-Vector3.up * touch.deltaPosition.x * _touchRotationSpeed * Time.deltaTime);               
                    break;
            }
        }
    }

    private void OnElementTouched(Element element)
    {
        Debug.Log($"{element.name} is touched!");
        UnSelectCurrentElement();
        SelectElement(element);
    }
    public void UnSelectAllElement()
    {
        UnSelectCurrentElement();
        foreach(Element element in _elements)
        {
            element.gameObject.SetActive(true);
            element.OnDeselect();
        }
    }
    private void UnSelectCurrentElement()
    {
        if (_currentElement != null)
        {
            _currentElement.transform.position = _currentElementLastPosition;
            _currentElement = null;
        }
    }
    private void SelectElement(Element selectedElement)
    {
        foreach (Element element in _elements)
        {
            if(element != selectedElement)
                element.gameObject.SetActive(false);
        }

        _currentElement = selectedElement;
        _currentElementLastPosition = selectedElement.transform.position;
        selectedElement.OnSelect();
        ZoomOnElement(selectedElement);
    }
    private void ZoomOnElement(Element element)
    {
        element.transform.position = zoomedTransform.position;
    }
}

