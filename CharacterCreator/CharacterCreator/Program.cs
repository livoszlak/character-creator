using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CharacterCreator.DataModels;
using CharacterCreator.Services;
using CharacterCreator.Application;

class Program
{
    static async Task Main(string[] args)
    {
        var characterCreator = new CharacterCreator.Application.CharacterCreator();
        await characterCreator.RunAsync();
        
        
        /*ClassService classService = new ClassService();
        List<ClassDataModel> classNames = await classService.GetClassInfo();

        foreach (var className in classNames)
        {
            Console.WriteLine(className.Name);
        }
        

        RaceService raceService = new RaceService();
        List<RaceDataModel> raceNames = await raceService.GetRaceInfo();
        
        foreach (var raceName in raceNames)
        {
            Console.WriteLine(raceName.Name);
        }*/
    }
}
