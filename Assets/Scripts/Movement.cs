using System;
using UnityEngine;

namespace Ulf
{
    public class Movement : MonoBehaviour
    {
        protected IControl control;

        private void Start()
        {
            control.OnMove += MoveDirect;
        }

        private void MoveDirect(int direct)
        {

        }
    }
}