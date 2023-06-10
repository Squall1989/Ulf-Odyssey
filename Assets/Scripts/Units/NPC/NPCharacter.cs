using System.Threading.Tasks;
using Ulf;

public class NPCharacter 
{
    private Planet planet;
    private CharacterStruct character;
    private Unit unit;
    private AgrType agrType;

    public NPCharacter(Planet planet, Unit unit, CharacterStruct character)
    {
        this.planet = planet;
        this.character = character;
        this.unit = unit;
        agrType = character.startAgr;
    }

    private async void UpdateAction()
    {
        while (unit.Attackable.IsAlive)
        {
            if(agrType == AgrType.agressive)
            {
                CheckTargets();
                await Task.Delay(1500);
            }
        }
    }

    private void CheckTargets()
    {
        var movables = planet.GetAttackables(AttackableType.player, unit.Movable.Position, character.interactDist);
        
    }
}
