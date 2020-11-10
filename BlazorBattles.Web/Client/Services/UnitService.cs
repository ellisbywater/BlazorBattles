using BlazorBattles.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBattles.Web.Client.Services
{
    public class UnitService : IUnitService
    {
        public IList<Unit> Units { get; } = new List<Unit>
        {
            new Unit {Id = 1, Title="Knight", Attack = 10, Defense=10, BananaCost = 100 },
            new Unit {Id = 2, Title="Mage", Attack = 20, Defense=1, BananaCost = 200 },
            new Unit {Id = 3, Title="Archer", Attack = 15, Defense=5, BananaCost = 150 }
        };

        public IList<UserUnit> UserUnits { get; set; } = new List<UserUnit>();

        public void AddUnit(int unitId)
        {
            Unit unit = Units.First(unit => unit.Id == unitId);
            UserUnits.Add(new UserUnit { UnitId = unit.Id, HitPoints = unit.HitPoints });
        }
    }
}
