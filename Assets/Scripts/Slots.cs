using UnityEngine;

public class Slots : MonoBehaviour
{
    public SpriteRenderer firstFakeSlotTop;
    public SpriteRenderer firstFakeSlotBottom;

    public SpriteRenderer secondFakeSlotTop;
    public SpriteRenderer secondFakeSlotBottom;

    public SpriteRenderer thirdFakeSlotTop;
    public SpriteRenderer thirdFakeSlotBottom;

    [SerializeField] private SpriteRenderer _firstSlot;
    [SerializeField] private SpriteRenderer _secondSlot;
    [SerializeField] private SpriteRenderer _thirdSlot;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private Transform _upperPoint;

    [SerializeField] private GameObject _threeInRow;

    [SerializeField] private SlotSO[] _slotSOArray;

    [SerializeField] private float _speenSpeed = 20f;

    [SerializeField] private Transform _gameSpawnPoint;

    public bool isSpin = false;

    private SlotSO _firstSlotSO;
    private SlotSO _secondSlotSO;
    private SlotSO _thirdSlotSO;

    private SlotSO _firstFakeSlotSOTop;
    private SlotSO _firstFakeSlotSOBottom;

    private SlotSO _secondFakeSlotSOTop;
    private SlotSO _secondFakeSlotSOBottom;

    private SlotSO _thirdFakeSlotSOTop;
    private SlotSO _thirdFakeSlotSOBottom;

    private bool _firstSlotUsed = false;
    private bool _secondSlotUsed = false;
    private bool _thirdSlotUsed = false;

    private Vector2 _startTouchPos = Vector2.zero;
    private float _swipeThreshold = 500f;

    private bool _isFXOnce = false;
    private bool _isSlotSoundPlay = false;

    private AudioSource _slotSound;

    private void Start()
    {
        _slotSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        SlotMonitoring();
        CheckSwipe();
    }

    private void CheckSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float deltaY = touch.position.y - _startTouchPos.y;

                if (deltaY > _swipeThreshold && !isSpin)
                {
                    isSpin = true;
                }
                else if (deltaY < -_swipeThreshold && isSpin)
                {
                    isSpin = false;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _startTouchPos = Vector2.zero;
            }
        }
        
#if UNITY_EDITOR
        // Mouse Input
        // Check for the initial mouse button down event
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPos = Input.mousePosition; // Capture the start position
        }

        // Check if the mouse button is being held down
        if (Input.GetMouseButton(0))
        {
            float deltaY = Input.mousePosition.y - _startTouchPos.y; // Calculate vertical movement

            // Determine if the vertical movement exceeds the threshold to start or stop spinning
            if (!isSpin && deltaY > _swipeThreshold)
            {
                // Start spinning
                isSpin = true;
                Debug.Log("Start Spin");
                // Add your spin start logic here
            }
            else if (isSpin && deltaY < -_swipeThreshold)
            {
                // Stop spinning
                isSpin = false;
                Debug.Log("Stop Spin");
                // Add your spin stop logic here
            }
        }

        // Check for the mouse button release event
        if (Input.GetMouseButtonUp(0))
        {
            // Optionally, reset the start position and/or stop spinning
            _startTouchPos = Vector2.zero;
            if (isSpin)
            {
                isSpin = false;
                Debug.Log("Stop Spin");
                // Add any additional logic for stopping the spin here
            }
        }
#endif
    }

    private void SlotMonitoring()
    {
        if (isSpin)
        {
            if (!_isSlotSoundPlay)
            {
                _slotSound.Play();
                _isSlotSoundPlay = true;
            }

            _isFXOnce = false;
            if (Player.Instance.GetEnergy() > 0)
            {
                SpinFirstSlot();
                SpinSecondSlot();
                SpinThirdSlot();
            }

            if (_firstSlot.transform.position.y <= _bottomPoint.position.y)
            {
                _firstSlotUsed = true;

                _firstSlotSO = _slotSOArray[UnityEngine.Random.Range(0, _slotSOArray.Length)];
                _firstSlot.sprite = _firstSlotSO.sprite;
                _firstSlot.transform.position = new Vector2(_firstSlot.transform.position.x, _upperPoint.position.y);

                _firstFakeSlotSOBottom = GetFakeRandomSlotSO(_firstSlotSO);
                firstFakeSlotBottom.sprite = _firstFakeSlotSOBottom.sprite;

                _firstFakeSlotSOTop = GetFakeRandomSlotSO(_firstSlotSO);
                firstFakeSlotTop.sprite = _firstFakeSlotSOTop.sprite;
            }
            else if (_secondSlot.transform.position.y <= _bottomPoint.position.y)
            {
                _secondSlotUsed = true;

                _secondSlotSO = _slotSOArray[UnityEngine.Random.Range(0, _slotSOArray.Length)];
                _secondSlot.sprite = _secondSlotSO.sprite;
                _secondSlot.transform.position = new Vector2(_secondSlot.transform.position.x, _upperPoint.position.y);

                _secondFakeSlotSOBottom = GetFakeRandomSlotSO(_secondSlotSO);
                secondFakeSlotBottom.sprite = _secondFakeSlotSOBottom.sprite;

                _secondFakeSlotSOTop = GetFakeRandomSlotSO(_secondSlotSO);
                secondFakeSlotTop.sprite = _secondFakeSlotSOTop.sprite;
            }
            else if (_thirdSlot.transform.position.y <= _bottomPoint.position.y)
            {
                _thirdSlotUsed = true;

                _thirdSlotSO = _slotSOArray[UnityEngine.Random.Range(0, _slotSOArray.Length)];
                _thirdSlot.sprite = _thirdSlotSO.sprite;
                _thirdSlot.transform.position = new Vector2(_thirdSlot.transform.position.x, _upperPoint.position.y);

                _thirdFakeSlotSOBottom = GetFakeRandomSlotSO(_thirdSlotSO);
                thirdFakeSlotBottom.sprite = _thirdFakeSlotSOBottom.sprite;

                _thirdFakeSlotSOTop = GetFakeRandomSlotSO(_thirdSlotSO);
                thirdFakeSlotTop.sprite = _thirdFakeSlotSOTop.sprite;
            }
        }
        else
        {
            StopSlots();
        }
    }

    private void SpinFirstSlot()
    {
        _firstSlot.transform.Translate(Vector2.down * _speenSpeed * Time.deltaTime);
    }

    private void SpinSecondSlot()
    {
        _secondSlot.transform.Translate(Vector2.down * _speenSpeed * Time.deltaTime);
    }

    private void SpinThirdSlot()
    {
        _thirdSlot.transform.Translate(Vector2.down * _speenSpeed * Time.deltaTime);
    }

    private void StopSlots()
    {
        if (_isSlotSoundPlay)
        {
            _slotSound.Stop();
            _isSlotSoundPlay = false;
        }

        _firstSlot.transform.position = new Vector2(_firstSlot.transform.position.x, _startPoint.position.y);
        _secondSlot.transform.position = new Vector2(_secondSlot.transform.position.x, _startPoint.position.y);
        _thirdSlot.transform.position = new Vector2(_thirdSlot.transform.position.x, _startPoint.position.y);

        CheckThreeInRow();

        if (CheckThreeInRow() && !_isFXOnce)
        {
            Instantiate(_threeInRow, Vector2.zero, Quaternion.identity);
            _isFXOnce = true;

            if (_firstSlotUsed)
            {
                _firstSlotUsed = false;
                Player.Instance.UseEnergy();
                Invoke("UseFirstSlotAbility", 1);
            }

            if (_secondSlotUsed)
            {
                _secondSlotUsed = false;
                Invoke("UseSecondSlotAbility", 1.5f);
            }

            if (_thirdSlotUsed)
            {
                _thirdSlotUsed = false;
                Invoke("UseThirdSlotAbility", 2f);
            }
        }
        else
        {
            if (_firstSlotUsed)
            {
                _firstSlotUsed = false;
                Player.Instance.UseEnergy();
                UseFirstSlotAbility();
            }

            if (_secondSlotUsed)
            {
                _secondSlotUsed = false;
                Invoke("UseSecondSlotAbility", 0.5f);
            }

            if (_thirdSlotUsed)
            {
                _thirdSlotUsed = false;
                Invoke("UseThirdSlotAbility", 1f);
            }
        }
    }

    private void UseFirstSlotAbility()
    {
        UseSlotAbility(_firstSlotSO);
    }

    private void UseSecondSlotAbility()
    {
        UseSlotAbility(_secondSlotSO);
    }

    private void UseThirdSlotAbility()
    {
        UseSlotAbility(_thirdSlotSO);
    }

    private SlotSO GetFakeRandomSlotSO(SlotSO slot)
    {
        SlotSO fakeSlotSO;

        do
        {
            fakeSlotSO = _slotSOArray[UnityEngine.Random.Range(0, _slotSOArray.Length)];
        } while (fakeSlotSO == slot);

        return fakeSlotSO;
    }

    private void UseSlotAbility(SlotSO slotSO)
    {
        if (slotSO != null)
        {
            bool isThreeInRow = CheckThreeInRow();

            GameObject fxInstance = Instantiate(slotSO.fx, _gameSpawnPoint.position, Quaternion.identity);
            fxInstance.transform.parent = _gameSpawnPoint;

            switch (slotSO.tipe)
            {
                case SlotSO.Tipe.Fruits:
                    Player.Instance.WinMoney(isThreeInRow ? slotSO.amount * 2 : slotSO.amount);
                    break;
                case SlotSO.Tipe.Cards:
                    Player.Instance.WinMoney(isThreeInRow ? slotSO.amount * 2 : slotSO.amount);
                    break;
                case SlotSO.Tipe.Bar:
                    Player.Instance.LoseMoney(isThreeInRow ? slotSO.amount * 2 : slotSO.amount);
                    break;
                case SlotSO.Tipe.Stuf:
                    Player.Instance.WinMoney(isThreeInRow ? slotSO.amount * 2 : slotSO.amount);
                    break;
            }
        }
    }


    private bool CheckThreeInRow()
    {
        if (_firstSlotSO != null && _secondSlotSO != null && _thirdSlotSO != null)
        {
            int firstIndex = _firstSlotSO.index;
            int secondIndex = _secondSlotSO.index;
            int thirdIndex = _thirdSlotSO.index;
            return firstIndex == secondIndex && secondIndex == thirdIndex;
        }
        else
        {
            return false;
        }
    }
}