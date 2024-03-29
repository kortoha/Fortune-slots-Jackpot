using UnityEngine;

namespace MiniGame
{
    public class MiniGame : MonoBehaviour
    {
        public bool isWheelSpin = false;

        [SerializeField] private Transform _wheel;
        [SerializeField] private GameObject _tutor;

        private Vector2 _startTouchPos = Vector2.zero;
        private float _swipeThreshold = 100f;

        private float _spinForce;
        private bool _isFirstSpin = true;

        private bool _isSlotSoundPlay = false;

        private AudioSource _slotSound;

        private void Start()
        {
            _slotSound = GetComponent<AudioSource>();
        }

        private void Update()
        {
            CheckSwipe();
            WheelSpinMonitoring();

            _tutor.SetActive(_isFirstSpin);
        }

        private void CheckSwipe()
        {
            // Touch input
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

                    if (deltaY > _swipeThreshold && !isWheelSpin)
                    {
                        isWheelSpin = true;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _startTouchPos = Vector2.zero;
                }
            }

#if UNITY_EDITOR
            // Mouse input
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _startTouchPos = Input.mousePosition; // Capture the start position
                }
                else if (Input.GetMouseButton(0))
                {
                    float deltaY = Input.mousePosition.y - _startTouchPos.y; // Calculate vertical movement

                    // Determine if the vertical movement exceeds the threshold to start the wheel spin
                    if (deltaY > _swipeThreshold && !isWheelSpin)
                    {
                        isWheelSpin = true;
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _startTouchPos = Vector2.zero;
                }
            }
#endif
        }

        private void WheelSpinMonitoring()
        {
            if (isWheelSpin)
            {
                if (!_isSlotSoundPlay)
                {
                    _slotSound.Play();
                    _isSlotSoundPlay = true;
                }

                _isFirstSpin = false;
                if (_spinForce > 0)
                {
                    _wheel.Rotate(transform.forward * -_spinForce);
                    _spinForce -= Time.deltaTime * 4;
                }
                else
                {
                    isWheelSpin = false;
                }
            }
            else
            {
                if (_isSlotSoundPlay)
                {
                    _slotSound.Stop();
                    _isSlotSoundPlay = false;
                }

                _spinForce = Random.Range(10, 20);
            }
        }

        private void OnEnable()
        {
            _isFirstSpin = true;
        }

        public bool IsFirsSpin()
        {
            return _isFirstSpin;
        }
    }
}