using DG.Tweening;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
namespace Ulf
{
    public class PiecesAnimStatic : MonoBehaviour
    {
        [Inject] private ElementPiecesScriptable[] elementPieces;

        protected PiecesPool _piecesPool;

        public void Init(PiecesPool piecesPool)
        {
            _piecesPool = piecesPool;
        }

        public List<Image> HealthDestroy(Vector2 pos, ElementType element,
            bool pieceAnimFly = false, bool isWorldPos = false)
        {
            var pieces = LookForPieces(element);

            List<Image> result = new List<Image>();

            for (int i = 0; i < pieces.Length; i++)
            {
                Vector2 piecPos = pieces[i].position;

                if (isWorldPos)
                {
                    piecPos *= .007f;
                }

                Image pieceImg = _piecesPool.GetPiece();
                RectTransform tr = (pieceImg.transform as RectTransform);

                pieceImg.enabled = true;
                pieceImg.sprite = pieces[i].sprite;
                
                if (isWorldPos)
                {
                    tr.localPosition = pos + piecPos;
                    tr.sizeDelta = pieces[i].size * .007f;
                }
                else
                {
                    tr.anchoredPosition = pos + piecPos;
                    tr.sizeDelta = pieces[i].size;
                }
                if(pieceAnimFly)
                    AnimFly(pieceImg, piecPos);

                result.Add(pieceImg);
            }

            return result;
        }

        protected void AnimFly(Image piece, Vector2 deltaPos)
        {
            RectTransform tr = piece.rectTransform;
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

        protected ElementPieceStart[] LookForPieces(ElementType element)
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