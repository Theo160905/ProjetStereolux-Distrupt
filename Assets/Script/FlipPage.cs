using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

// Alias EnhancedTouch.Touch to "Touch" for less typing.
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class FlipPage : MonoBehaviour
{
    //[SerializeField] private PlayerInput _playerInput;
    //private InputAction _touchAction;
    [SerializeField] private MeshRenderer _WhereToPutTexture1;
    private Texture2D _tex2D;
    //[SerializeField] private Camera _nextCamera;
    public Animator animator;
    [SerializeField] string _wichAnimationToPlay;
    [SerializeField] private GameObject _currentLevelToUnLoad;
    [SerializeField] private GameObject _nextLevelToLoad;

    //For later
    [Tooltip ("To know if the player finished the level.")]public bool IsLevelFinished;
    [Tooltip("To know wich level we are in.")] public int WichCurrentLevel;

    //public bool DidLevelEnd = false;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    public bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    void Awake()
    {
        //_WhereToPutTexture1.gameObject.transform.position = new Vector3(30, 1, -5);
        
        //_nextCamera.gameObject.SetActive(true);
        //_touchAction = _playerInput.actions["Attack"];
    }

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
                    StartCoroutine(TakeScreenShot());
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    _endTouchPosition = touch.screenPosition;
                    //Debug.Log(_endTouchPosition);

                    if (_endTouchPosition.x < _startTouchPosition.x) // Swipe to the left
                    {
                        _wichAnimationToPlay = "Left";
                        MatchGameObjectToScreenSize(_WhereToPutTexture1.gameObject);
                        PlayAnimation(_wichAnimationToPlay);
                    }

                    if (_endTouchPosition.x > _startTouchPosition.x) // Swipe to the right
                    {
                        _wichAnimationToPlay = "Right";
                        PlayAnimation(_wichAnimationToPlay);
                    }
                    break;
            }
        }
    }

    public void PlayAnimation(string animationName)
    {
        animator.enabled = true;
        animator.SetTrigger(animationName);
    }

    public IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();
        if (_tex2D == null)
        {
            _tex2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            _tex2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            _tex2D.Apply();
            _WhereToPutTexture1.material.mainTexture = _tex2D;
            _WhereToPutTexture1.gameObject.SetActive(true);
        }
    }

    public void MatchGameObjectToScreenSize(GameObject go)
    {
        float GameObjectToCameraDistance = Vector3.Distance(go.transform.position, Camera.main.transform.position);
        float GameObjectHeightScale = (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * GameObjectToCameraDistance) / 1f; // GameObject is 1 Unit in x and y, if scale units change, then change this value.
        float GameObjectWidthScale = GameObjectHeightScale * Camera.main.aspect;
        go.transform.localScale = new Vector3(GameObjectWidthScale, GameObjectHeightScale, 0.01f);
    }
}
