using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MatchItItemTest : MonoBehaviour
//, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // TODO: Create a randmozier for the game (numberof items, colors, types, categories, names, etc...)
    // TODO: Check mobile compatibiilty with the game
    [SerializeField] private AudioSource _source;
    [SerializeField] public Vector2 _pickUpSize;
    [SerializeField] private AudioClip _pickUpClip, _dropClip;
    [SerializeField] public MatchItGameManager _ItemCollections;

    public Canvas _parentCanvas;
    public GameObject _parentItemCollection;
    private bool isDragging;
    public bool _isMatched = false;
    private Vector2 _orginalSize;
    public Image _originalImage;
    public float _distanceToMatch;
    private LayoutElement _layoutElement;
    private BoxCollider2D _imageBoxCollider;

    private Vector2 _offset, _originalPosition;

    //! the OnMouseDown() trigger is not working, serious issue with the code
    //! had to expose the OnMouseUp & OnMouseDown to go with an event trigger solution
    //TODO: find a more suitable solution for unity OnMouseDown & OnMouseUp function
    void Awake()
    {
        _originalPosition = transform.position;
        // _originalImage = GetComponent<Image>();
        _orginalSize = GetComponent<Image>().rectTransform.sizeDelta;
        _layoutElement = GetComponent<LayoutElement>();
        _imageBoxCollider = GetComponent<BoxCollider2D>();
        _originalImage = GetComponent<Image>();
    }
    void Update()
    {
        if (!isDragging)
        {
            return;
        }
        else if (isDragging)
        {
            var mousePosition = Input.mousePosition;
            //! Below code is causing major offset, need to fix but now doing a workaround
            // var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
            // Debug.Log(mousePosition.ToString());
            // Debug.Log(this.GetComponent<Image>().rectTransform.sizeDelta.ToString());
            // Debug.Log(_pickUpSize.ToString());
            // transform.position = mousePosition - _offset;
            // Debug.Log(transform.localPosition.ToString());
        }
    }

    public void OnMouseDown()
    {
        //TODO: Add pickup sound
        Debug.Log("Gotcha");
        isDragging = true;
        transform.SetParent(_parentCanvas.transform);
        _layoutElement.enabled = false;
        _originalImage.rectTransform.sizeDelta = _pickUpSize;
        _imageBoxCollider.size = _pickUpSize;

        // transform.localScale = new Vector3(1f, 1f, 1f);
        _source.PlayOneShot(_pickUpClip);
        //* This adjusts the pickup location offset (you hold the object from the part you clicked, no auto snap to center of mouse)
        // _offset = GetMousefPosition() - (Vector2)transform.position;
    }

    public void OnMouseUp()
    {
        _ItemCollections.DroppedItem();
        if (_isMatched)
        {
            DestoryItem();
        }
        else
        {
            //TODO: Add drop sound
            Debug.Log("Comeback");
            isDragging = false;
            transform.position = _originalPosition;
            transform.SetParent(_parentItemCollection.transform);
            _layoutElement.enabled = true;
            _originalImage.rectTransform.sizeDelta = _orginalSize;
            _imageBoxCollider.size = _orginalSize;
            // GetComponent<Image>().rectTransform.sizeDelta = new Vector2(150f, 150f);
        }
    }

    Vector2 GetMousefPosition()
    {
        return Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }
    void DestoryItem()
    {
        Destroy(gameObject);
    }
}
