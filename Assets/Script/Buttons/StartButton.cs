using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject _falsePage;
    //private Texture2D _tex2D;

    //[SerializeField] private Animator _animator;
    [SerializeField] string _wichAnimationToPlay;

    [SerializeField] private GameObject _rootMenuGame;
    [SerializeField] private GameObject _nextLevelToLoad;

    [SerializeField] private FlipPage _flipPage;

    public IEnumerator StartGame()
    {
        StartCoroutine(_flipPage.TakeScreenShot());
        yield return new WaitForSeconds(0.05f);
        _rootMenuGame.SetActive(false);
        _flipPage.MatchGameObjectToScreenSize(_falsePage);
        _flipPage.PlayAnimation(_wichAnimationToPlay);
        _nextLevelToLoad.SetActive(true);
        /*
        if (_flipPage.AnimatorIsPlaying() == false)
        {
            _nextLevelToLoad.SetActive(true);
        }
        */
    }

    public void PlayCoroutine()
    {
        StartCoroutine(StartGame());
    }
}
