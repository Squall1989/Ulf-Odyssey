using System.Threading.Tasks;
using Ulf;

public class NPCharacter 
{
    private Planet planet;
    private CharacterStruct character;
    private Unit unit;
    private AgrType agrType;

    public Unit Unit => unit;

    public NPCharacter(Unit unit, CharacterStruct character)
    {
        planet = unit.Planet;
        this.character = character;
        this.unit = unit;
        agrType = character.startAgr;

        UpdateAction();
    }

    private async void UpdateAction()
    {
        //while (unit.Attackable.IsAlive)
        //{
        //    if(agrType == AgrType.agressive)
        //    {
        //        CheckTargets();
        //        await Task.Delay(1500);
        //    }
        //}
    }

    private void CheckTargets()
    {
        var movables = planet.GetAttackables(AttackableType.player, unit.Movable.Position, character.interactDist);
        
    }
}