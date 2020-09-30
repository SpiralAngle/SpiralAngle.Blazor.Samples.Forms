using BlazorFormSample.Client.SharedComponent;
using BlazorFormSample.Shared.CreatureModels;
using BlazorFormSample.Shared.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.Creatures
{
    public class CreatureEditModel : EditModel<Creature>
    {
        protected IService<GameSystem> GameSystemService {get; private set;}
        public CreatureEditModel(IService<Creature> service, IService<GameSystem> gameSystemService) : base(service)
        {
            GameSystemService = gameSystemService;
        }

        public GameSystem GameSystem { get; set; }

        protected override async Task ExtendedLoadModel()
        {
            GameSystem = await GameSystemService.GetAsync(Model.GameSystemId);
        }
    }
}
