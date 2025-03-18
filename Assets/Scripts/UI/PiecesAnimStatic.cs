using DG.Tweening;
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

        public void HealthDestroy(Vector2 pos, ElementType element)
        {
            var pieces = LookForPieces(element);

            for(int i = 0; i < pieces.Length; i++)
            {
                Image pieceImg = _piecesPool.GetPiece();
                RectTransform tr = (pieceImg.transform as RectTransform);

                pieceImg.enabled = true;
                tr.anchoredPosition = pos + pieces[i].position;
                pieceImg.sprite = pieces[i].sprite;
                tr.sizeDelta = pieces[i].size;

                AnimFly(pieceImg, tr, pieces[i].position);
            }
        }

        private void AnimFly(Image piece, RectTransform tr, Vector2 deltaPos)
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