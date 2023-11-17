
public class SinglePlayerGame : IGame
{
    private PlayerData _playerData;

    public void RegisterPlayer(PlayerData playerData)
    {
        _playerData = playerData;
    }
}
