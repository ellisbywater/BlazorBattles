using BlazorBattles.Web.Shared;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorBattles.Web.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toast;
        private readonly HttpClient _httpClient;

        public UnitService(IToastService toast, HttpClient httpClient)
        {
            _toast = toast;
            _httpClient = httpClient;
        }
        public IList<Unit> Units { get; set;  } =  new List<Unit>();

        public IList<UserUnit> UserUnits { get; set; } = new List<UserUnit>();

        public void AddUnit(int unitId)
        {
            Unit unit = Units.First(unit => unit.Id == unitId);
            UserUnits.Add(new UserUnit { UnitId = unit.Id, HitPoints = unit.HitPoints });
            _toast.ShowSuccess($"A {unit.Title} has joined your army!", "Unit recruited!");
        }

        public async Task LoadUnitsAsync()
        {
            if (Units.Count == 0)
            {
                Units = await _httpClient.GetFromJsonAsync<IList<Unit>>("api/unit");
            }
        }
    }
}
