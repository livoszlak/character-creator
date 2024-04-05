using CharacterCreator.DataModels;

namespace CharacterCreator.Services
{
    public class RaceService : ApiService
    {
        public static async Task<List<RaceNamesModel>> GetRaces()
        {
            RaceService raceService = new RaceService();
            string endpoint = "races";
            var response = await raceService.GetAsync<RaceNamesModelWrapper>(endpoint);
            return response.Results;
        }
        
        private async Task<RaceDataModel> GetRaceData(Character character)
        {
            string endpoint = $"races/{character.Race.ToLower()}";
            var response = await GetAsync<RaceDataModel>(endpoint);
            return response;
        }
        
        public static async Task<RaceDataModel> GetRaceDetails(Character character)
        {
            RaceService raceService = new RaceService();
            return await raceService.GetRaceData(character);
        }

    }
}