using DG.Tweening;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
namespace Ulf
{
    public class PiecesAnimStatic : MonoBehaviour
    {
        [Inject] private ElementPiecesScriptable[] elementPieces;

        private PiecesPool _piecesPool;

        public void Init(PiecesPool piecesPool)
        {
            _piecesPool = piecesPool;
        }

        public void HealthDestroy(Vector2 pos, ElementType element, Vector2 flyImpulse = default, bool isWorldPos = false)
        {
            var pieces = LookForPieces(element);


            for (int i = 0; i < pieces.Length; i++)
            {
                Vector2 piecPos = pieces[i].position;

                if (isWorldPos)
                {
                    piecPos *= .01f;
                }

                Image pieceImg = _piecesPool.GetPiece();
                RectTransform tr = (pieceImg.transform as RectTransform);

                pieceImg.enabled = true;
                pieceImg.sprite = pieces[i].sprite;
                
                if (isWorldPos)
                {
                    tr.localPosition = pos + piecPos;
                    tr.sizeDelta = pieces[i].size * .01f;
                }
                else
                {
                    tr.anchoredPosition = pos + piecPos;
                    tr.sizeDelta = pieces[i].size;
                }

                AnimFly(pieceImg, tr, piecPos + flyImpulse);
            }
        }

        protected virtual void AnimFly(Image piece, RectTransform tr, Vector2 deltaPos)
        {
            Vector2 pos = tr.position;
            tr.DOMove(pos + deltaPos, .3f).onComplete += () => 
            {
                piece.DOFade(0, .1f).onComplete += () =>
                {
                    piece.DOFade(1, 0);
                    _piecesPool.ReturnPiece(piece);
                };
            };
        }

        private ElementPieceStart[] LookForPieces(ElementType element)
        {
            for (int i = 0; i < elementPieces.Length; i++)
            {
                if (elementPieces[i].Element == element)
                {
                    return elementPieces[i].Pieces;
                }
            }

            return null;
        }
    }
}