using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ulf
{
    public class EnemyHpUiWorld : PiecesAnimStatic
    {
        [SerializeField] private PiecesPool poolPieces;
        [Inject] private ElementSpritesScriptable sprites;
        [Inject] private WorldView worldView;
        [Inject] private GameState gameState;

        private void Start()
        {
            gameState.OnChangeCondition += GameStateChange;
            worldView.OnUnitDamaged += AnimDamageWorld;
            Init(poolPieces);
        }

        private void AnimDamageWorld(Unit damager, Unit attackable, CapsuleCollider2D capsCollider)
        {
            var path = CalculateFlyPath(damager, attackable, capsCollider);
            ElementType element = attackable.Health.Element;
            var pieces = HealthDestroy(path[0], element, false, true);
            var poses = LookForPieces(element).Select(p => p.position);

            TrajectoryAnim(pieces, path, poses);
        }

        private void TrajectoryAnim(List<Image> pieces, Vector3[] path, IEnumerable<Vector2> poses)
        {
            float trajectoryTime = .6f;
            float fadeyTime = .2f;

            var numerator = poses.GetEnumerator();
            for (int i = 0; i < pieces.Count; i++)
            {
                numerator.MoveNext();
                var pos = numerator.Current * 0.007f;
                Vector3[] piecePath = new Vector3[path.Length];
                path.CopyTo(piecePath, 0);

                for (int j = 0; j < piecePath.Length; j++)
                {
                    piecePath[j] += (Vector3)pos * (j +1);
                }
                var piece = pieces[i];
                piece.rectTransform.DOPath(piecePath, trajectoryTime, PathType.CatmullRom);

                
                piece.DOFade(0, fadeyTime).SetDelay(trajectoryTime - fadeyTime).onComplete += () =>
                {
                    piece.DOFade(1, 0);
                    _piecesPool.ReturnPiece(piece);
                };
            }
        }

        private Vector3[] CalculateFlyPath(Unit damager ,Unit attackable, CapsuleCollider2D collider2D)
        {
            Vector3 pos = attackable.Move.Position;
            Vector3 up = math.normalize(pos - (Vector3)attackable.Move.PlanetPosition);
            Vector3 dir = math.normalize(pos - (Vector3)damager.Move.Position);

            float height = 2f;
            float width = 1f;

            if(collider2D != null)
            {
                height = collider2D.offset.y;
                width = collider2D.size.x;
            }


            float rndY = UnityEngine.Random.Range(height, height * 2f);
            float rndDist = UnityEngine.Random.Range(1f, 2f);
            float rndHeight = UnityEngine.Random.Range(.5f, 1f);

            Vector3 startPos = pos + up * rndY + dir * width;
            Vector3 peakPos = startPos + rndHeight * up + rndDist * dir * .5f;
            Vector3 endPos = startPos + rndDist * dir;

            return new Vector3[]
            {
                startPos,
                peakPos,
                endPos,
            };
        }

        private void GameStateChange(GameCondition condition)
        {
            switch(condition)
            {
                case GameCondition.allIsReady:
                    worldView.Init();
                    break;
            }
        }
    }
}