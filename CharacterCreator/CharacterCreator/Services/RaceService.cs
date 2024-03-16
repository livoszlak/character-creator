using CharacterCreator.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharacterCreator.Services
{
    public class RaceService : ApiService
    {
        public RaceService() : base()
        {
        }

        public async Task<List<RaceDataModel>> GetRaceNames()
        {
            string endpoint = "races";
            var response = await GetAsync<RaceModelWrapper>(endpoint);
            return response.Results;
        }
    }
}