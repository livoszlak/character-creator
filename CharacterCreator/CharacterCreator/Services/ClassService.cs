using CharacterCreator.DataModels;

namespace CharacterCreator.Services;

public class ClassService : ApiService
{
    public static async Task<List<ClassNamesModel>> GetClasses()
    {
        ClassService classService = new ClassService();
        string endpoint = "classes";
        var response = await classService.GetAsync<ClassNamesModelWrapper>(endpoint);
        return response.Results;
    }
    
    private async Task<ClassDataModel> GetClassData(Character character)
    {
        string endpoint = $"classes/{character.Class.ToLower()}";
        var response = await GetAsync<ClassDataModel>(endpoint);
        return response;
    }
    
    public static async Task<ClassDataModel> GetClassDetails(Character character)
    {
        ClassService classService = new ClassService();
        return await classService.GetClassData(character);
    }
}