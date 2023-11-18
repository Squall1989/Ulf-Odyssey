using MsgPck;
using System;
using System.Collections.Generic;

namespace Ulf
{
    public class RelayBase
    {
        protected delegate void UnionDelegate(IUnionMsg message);
        protected Dictionary<Type, UnionDelegate> callbacksDict = new ();


        protected void SetHandler<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (callbacksDict.ContainsKey(type))
            {
                callbacksDict[type] += (msg) => callback((T)msg);
            }
            else
            {
                callbacksDict.Add(type, (msg) => callback((T)msg));
            }
        }
    }
}