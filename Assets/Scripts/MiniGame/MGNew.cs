using System;
using Unity.VisualScripting;
using UnityEngine;

public class MGNew : MonoBehaviour
{
    public static MGNew Instance { get; private set; }

    public Chip chipPrefab;
    public GameObject rotators;
    public GameObject tutorial;

    [NonSerialized]public bool isMove = false;

    [SerializeField] private Transform[] _obstaclesPoints;
    [SerializeField] private GameObject[] _obstaclesArray;

    public float moveSpeed = 5f;

    private Chip _targetObject;
    private bool _isFirstTouch = false;
    private bool _isFirstRelease = true;

    [SerializeField] private Collider _targetCollider;
    private bool _isCreated = false;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _isFirstTouch = false;
            if (!_isCreated && !IsTouchInsideCollider(mousePosition))
            {
                CreateChip(mousePosition);
            }
            rotators.SetActive(true);
            MoveObject(mousePosition);
        }
        else if (Input.touchCount > 0)
        {
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            _isFirstTouch = false;
            if (!_isCreated && !IsTouchInsideCollider(touchPosition))
            {
                CreateChip(touchPosition);
            }
            MoveObject(touchPosition);
        }
        else
        {
            if (_isFirstRelease)
            {
                if (_targetObject != null)
                {
                    _isFirstTouch = true;
                    ReleaseObject();
                }
            }
        }
    }

    private bool IsTouchInsideCollider(Vector3 touchPosition)
    {
        if (_targetCollider != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (_targetCollider.Raycast(ray, out hit, float.MaxValue))
            {
                return true;
            }
        }
        return false;
    }

    private void MoveObject(Vector3 position)
    {
        isMove = true;
        if (_targetCollider != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (_targetCollider.Raycast(ray, out hit, float.MaxValue))
            {
                return;
            }
        }

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector3 topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, screenHeight, 0));
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, 0, 0));

        position.x = Mathf.Clamp(position.x, topLeft.x, bottomRight.x);
        position.y = Mathf.Clamp(position.y, bottomRight.y, topLeft.y);

        if (_targetObject != null)
        {
            _targetObject.transform.position = Vector2.Lerp(_targetObject.transform.position, position, moveSpeed * Time.deltaTime);
        }
    }

    private void ReleaseObject()
    {
        rotators.SetActive(false);
        _targetObject.isRelease = true;

        if (_targetCollider != null)
        {
            _targetCollider.isTrigger = true;
        }

        _targetObject = null;
        _isCreated = false;
        isMove = false;
    }

    private void CreateChip(Vector3 position)
    {
        tutorial.SetActive(false);
        _targetObject = Instantiate(chipPrefab, new Vector2(position.x, position.y), Quaternion.identity);
        _isCreated = true;
    }

    private void OnDisable()
    {
        tutorial.SetActive(true);
    }

    private void OnEnable()
    {
        Instance = this;

        if (_obstaclesArray != null && _obstaclesArray.Length > 0 && _obstaclesPoints != null && _obstaclesPoints.Length > 0)
        {
            bool[] usedPoints = new bool[_obstaclesPoints.Length];

            for (int i = 0; i < _obstaclesArray.Length; i++)
            {
                int randomPointIndex;
                do
                {
                    randomPointIndex = UnityEngine.Random.Range(0, _obstaclesPoints.Length);
                } while (usedPoints[randomPointIndex]);

                _obstaclesArray[i].transform.position = _obstaclesPoints[randomPointIndex].position;

                usedPoints[randomPointIndex] = true;
            }
        }
    }
}
