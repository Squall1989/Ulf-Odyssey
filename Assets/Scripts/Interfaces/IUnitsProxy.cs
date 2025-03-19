
using System.Collections.Generic;
using Ulf;

public interface IUnitsProxy 
{
    void Add(Unit unit);

    List<Unit> GetUnits(IRound round);
}
