

using System.Collections.Generic;

namespace Ulf
{
    /// <summary>
    /// Control part of game scene (Or whole scene)
    /// Use in single and multiplayer modes
    /// </summary>
    public class SceneController
    {
        List<IRound> roundList = new();
        List<IControl> controlList = new();

        public void AddControllable(IRound round)
        {
            roundList.Add(round);
        }

        public void AddControllable(IControl control)
        {
            controlList.Add(control);
        }
    }
}