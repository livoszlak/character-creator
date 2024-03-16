using CharacterCreator.DataModels;
using CharacterCreator.Services;

namespace CharacterCreator.Application;

public class CharacterCreator
{
    public async Task RunAsync()
    {
        Character character = new Character();
        var availableRaces = await GetRaces();
        var availableClasses = await GetClasses();
        
        
        
    }
    
    private static async Task<List<ClassDataModel>> GetClasses()
    {
        ClassService classService = new ClassService();
        return await classService.GetClassNames();
    }

    private static async Task<List<RaceDataModel>> GetRaces()
    {
        RaceService raceService = new RaceService();
        return await raceService.GetRaceNames();
    }

    private void ChooseName(Character character)
    {
        Console.Write("Enter your character name: ");
        string name = Console.ReadLine();
        character.Name = name;
        Console.WriteLine($"Welcome, {character.Name}");
    }
}