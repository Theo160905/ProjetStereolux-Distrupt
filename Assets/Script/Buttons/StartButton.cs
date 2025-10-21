using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class StartButton : MonoBehaviour
{
    [SerializeField] private MeshRenderer _WhereToPutTexture1;
    //private Texture2D _tex2D;

    //[SerializeField] private Animator _animator;
    [SerializeField] string _wichAnimationToPlay;

    [SerializeField] private GameObject _rootMenuGame;
    [SerializeField] private GameObject _nextLevelToLoad;

    [SerializeField] private FlipPage _flipPage;

    public void StartGame()
    {
        _flipPage.StartCoroutine(_flipPage.TakeScreenShot());
        _rootMenuGame.SetActive(false);
        _flipPage.MatchGameObjectToScreenSize(_WhereToPutTexture1.gameObject);
        _flipPage.PlayAnimation(_wichAnimationToPlay);
        /*
        if (_flipPage.AnimatorIsPlaying() == false)
        {
            _nextLevelToLoad.SetActive(true);
        }
        */
        _nextLevelToLoad.SetActive(true);
    }

}
