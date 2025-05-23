using MsgPck;
using System.Threading.Tasks;

namespace Ulf
{
    public class PlayerControlHost : PlayerControlBase, IPlayersProxy
    {
        private MultiplayerHost _multiplayerHost;
        private SceneGenerator _sceneGenerator;
        private INetworkable _networkable;

        public PlayerControlHost(SceneGenerator sceneGenerator, MultiplayerHost multiplayerHost, INetworkable networkable, StatsScriptable[] stats)
            : base(stats)
        {
            _multiplayerHost = multiplayerHost;
            _sceneGenerator = sceneGenerator;
            _networkable = networkable;
            _networkable.RegisterHandler<RequestPlayerSpawn>(PlayerRequestSpawn);
            _networkable.RegisterHandler<ActionData>(OtherPlayerAction);

            _multiplayerHost.OnPlayerReady += SendSnapshots;
        }

        private void PlayerRequestSpawn(RequestPlayerSpawn spawn, IConnectWrapper connect)
        {
            var spawnedPlayer = _sceneGenerator.SpawnPlayer();
            var id = _multiplayerHost.GetPlayerId(connect);

            if(id != null)
            {
                spawnedPlayer.playerId = id;
                _networkable.Send(spawnedPlayer);
                OnOtherPlayerSpawn?.Invoke(spawnedPlayer);
            }
        }

        private void PlayerSpawnUnit(SnapPlayerStruct playerStruct)
        {
            OnOtherPlayerSpawn?.Invoke(playerStruct);
            _networkable.Send(playerStruct);
        }

        public Task<SnapPlayerStruct> SpawnPlayer()
        {
            var playerStruct = _sceneGenerator.SpawnPlayer();

            _networkable.Send(playerStruct);
            
            return Task.FromResult(playerStruct);
        }

        protected void OtherPlayerAction(ActionData playerActionData)
        {
            _networkable.Send(playerActionData);
            base.DoPlayerAction(playerActionData);
        }

        protected override void OurPlayerAction(ActionData playerActionData)
        {
            base.OurPlayerAction(playerActionData);
            _networkable.Send(playerActionData);
        }

        protected void SendSnapshots(IConnectWrapper connection)
        {
            var players = PlayersSnapshot();
            foreach (var player in players)
                _networkable.Send(player, connection);
        }

        public override void AddPlayer(Player player, bool isOurPlayer)
        {
            base.AddPlayer(player, isOurPlayer);
            player.Actions.OnAttacked += CreateDamage;
        }
    }
}