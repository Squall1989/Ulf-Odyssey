using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileMono : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRender;
    [SerializeField] private Sprite _mainSprite;
    [SerializeField] private Sprite _hitSprite;
    [SerializeField] private CapsuleCollider2D _collider;
    [SerializeField] private float flySpeed;
    [SerializeField] private LayerMask obstaclesMask;

    private float _fallSpd;
    private float _fallDeg;
    private bool _isFly;
    private LayerMask _targetMask;

    public void Launch(LayerMask targetMask, Vector3 downVector)
    {
        _targetMask = targetMask;
        _isFly = true;
        _spriteRender.sprite = _mainSprite;

        float angle = Vector3.Angle(transform.right, downVector);
        _fallDeg = angle;
        _fallSpd = 0;
        UnityEngine.Debug.Log("Missile fall angle: " + angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        bool isTarget = ((1 << layer) & _targetMask.value) != 0;
        bool isObstacle = ((1 << layer) & obstaclesMask.value) != 0;

        if (isTarget || isObstacle)
        {
            transform.parent = collision.transform;
            _spriteRender.sprite = _hitSprite;
            _isFly = false;
        }
    }

    void Update()
    {
        if (!_isFly)
        {
            return;
        }

        _fallSpd += Time.deltaTime;

        _fallDeg -= _fallSpd;

        if(_fallDeg > 0)
            transform.Rotate(new Vector3(0, 0, -_fallSpd));

        transform.position += transform.right * Time.deltaTime * flySpeed;
    }
}
