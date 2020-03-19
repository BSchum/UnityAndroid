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

    Element[] _elements;

    Element _currentElement = null;
    Vector3 _currentElementLastPosition;

    private void Start()
    {
        _elements = elementHolder.GetComponentsInChildren<Element>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                if(raycastHit.collider.CompareTag("Element"))
                    OnElementTouched(raycastHit.collider.gameObject.GetComponent<Element>());
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
            _currentElement.transform.position = _currentElementLastPosition;
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

