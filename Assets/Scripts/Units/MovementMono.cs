using UnityEngine;

namespace Ulf
{

    public class MovementMono : MonoBehaviour
    {
        protected CircleMove _circleMove;
        [SerializeField] private Transform _visualTransform;
        [SerializeField] protected Animator _animator;

        private Vector3 leftDir, rightDir;

        private int _enemyLayerMask;

        public CircleMove CircleMove => _circleMove;

        public virtual void Init(Planet planet, CircleMove circleMove, float angle)
        {
            _circleMove = circleMove;
            _circleMove.ToLand(planet, angle);
            _circleMove.SetMoveDirect(0);
            _circleMove.OnChangeSpeed += SetSpeed;

            transform.position = _circleMove.Position;

            circleMove.OnMoveDirect += ChangeDirect;

            rightDir = _visualTransform.localRotation.eulerAngles;
            leftDir = rightDir + new Vector3(0, 180f, 0);
            SetEnemyLayerMask(LayerMask.GetMask("player"));
        }

        private void ChangeDirect(int direct)
        {
            if(direct == -1)
            {
                _visualTransform.localRotation = Quaternion.Euler(leftDir);
            }
            else if (direct == 1)
            {
                _visualTransform.localRotation = Quaternion.Euler(rightDir);
            }
        }

        protected virtual float RotateUnit()
        {
           return _circleMove.Round.RoundMono.LookAtCenter(transform);

        }

        private void Update()
        {
            _circleMove.SetDeltaTime(Time.deltaTime);
            transform.position = _circleMove.Position;
            transform.Rotate(new Vector3(0,0,RotateUnit()));
        }

        internal void SetSpeed(float speed)
        {
            _animator.SetFloat("speed", speed / 10);
        }

        protected void SetEnemyLayerMask(int layerMask)
        {
            _enemyLayerMask = layerMask;
        }

        private bool IsEnemyLayer(Collider2D collider)
        {
            int layer = collider.gameObject.layer;
            bool inMask = (_enemyLayerMask & (1 << layer)) != 0;
            return inMask;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsEnemyLayer(collision))
            {
                _circleMove.RestrictMove(0);
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        { 
            MoveCollision(collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            MoveCollision(collision);
        }

        private void MoveCollision(Collider2D collision)
        {
            if (IsEnemyLayer(collision))
            {
                bool rightDirect = transform.InverseTransformPoint(collision.transform.position).x > 0;
                _circleMove.RestrictMove(rightDirect ? -1 : 1);
            }
        }
    }
}