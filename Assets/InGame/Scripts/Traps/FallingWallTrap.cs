using UnityEngine;
using DG.Tweening;

public class FallingWallTrap : MonoBehaviour
{

    private float _positionToMove;
    private float _localScaleY, _localPosY;

    private void Awake()
    {
        _localPosY =  transform.localPosition.y;
        _localScaleY = transform.localScale.y;
        _positionToMove = _localPosY - _localScaleY;
    }


    private void Start()
    {
        transform.DOLocalMoveY(_positionToMove, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InElastic).OnUpdate(() =>
        {
          
        });
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colliding");
        if(other.tag == "Player")
        {
            var health = other.GetComponent<PlayerHealth>();
            health.Kill();
        }
    }


}
