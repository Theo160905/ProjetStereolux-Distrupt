using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

// Alias EnhancedTouch.Touch to "Touch" for less typing.
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class ChangingLevel1 : MonoBehaviour
{
    [SerializeField] private FlipPage _flipPage;


    [SerializeField] private MeshRenderer _WhereToPutTexture1;

    public Animator animator;
    [SerializeField] string _wichAnimationToPlay;
    [SerializeField] private GameObject _currentLevelToUnLoad;
    [SerializeField] private GameObject _nextLevelToLoad;

    //For later
    [Tooltip("To know if the player finished the level.")] public bool IsLevelFinished;
    [Tooltip("To know wich level we are in.")] public int WichCurrentLevel;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private void OnEnable()
    {
        //_touchAction.performed += TouchPressed;
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        //_touchAction.performed -= TouchPressed;
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouchPosition = touch.startScreenPosition;
                    //Debug.Log(_startTouchPosition);
                    StartCoroutine(_flipPage.TakeScreenShot());
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    _endTouchPosition = touch.screenPosition;
                    //Debug.Log(_endTouchPosition);

                    if (_endTouchPosition.x < _startTouchPosition.x) // Swipe to the left
                    {
                        _wichAnimationToPlay = "Left";
                        _flipPage.MatchGameObjectToScreenSize(_WhereToPutTexture1.gameObject);
                        _flipPage.PlayAnimation(_wichAnimationToPlay);
                    }

                    if (_endTouchPosition.x > _startTouchPosition.x) // Swipe to the right
                    {
                        _wichAnimationToPlay = "Right";
                        _flipPage.PlayAnimation(_wichAnimationToPlay);
                    }
                    break;
            }
        }
    }
}
