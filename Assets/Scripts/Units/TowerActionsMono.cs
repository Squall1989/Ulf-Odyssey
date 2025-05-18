namespace Ulf
{
    public class TowerActionsMono : ActionsMono
    {
        private TowerBuildMono _build;

        public void Init(TowerBuildMono build)
        {
            _build = build;
        }

        public void BuildAction()
        {
            _build.InvokeAction(Action);
        }
    }
}