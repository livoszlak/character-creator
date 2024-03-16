using CharacterCreator.DataModels;

namespace CharacterCreator.Services;

public class ClassService : ApiService
{
    public ClassService() : base()
    {
    }

    public async Task<List<ClassDataModel>> GetClassNames()
    {
        string endpoint = "classes";
        var response = await GetAsync<ClassModelWrapper>(endpoint);
        return response.Results;
    }
}