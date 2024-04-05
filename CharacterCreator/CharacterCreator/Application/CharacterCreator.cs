using System.Text;
using CharacterCreator.DataModels;
using CharacterCreator.Services;
using Newtonsoft.Json;
using Spectre.Console;

namespace CharacterCreator.Application;

public class CharacterCreator
{
    public static List<string>? Classes { get; set; }
    public static List<string>? Races { get; set; }
    private static RaceDataModel? RaceDetails { get; set; }
    private static ClassDataModel? ClassDetails { get; set; }
    public static List<string> Commands { get; } = new List<string>
    {
        "create",
        "about",
        "quit",
        "menu",
        "print",
        "save"
    };
    
    /*private CharacterFactory characterFactory = new CharacterFactory();*/

    public CharacterCreator()
    {
        InitializeAsync().GetAwaiter().GetResult();
    }
    
    private static async Task InitializeAsync()
    {
        var availableClasses = await ClassService.GetClasses();
        var availableRaces = await RaceService.GetRaces();
        
        Classes = availableClasses.Select(classChoice => classChoice.Name).ToList();
        Races = availableRaces.Select(raceChoice => raceChoice.Name).ToList();
    }
    
    public async Task Run()
    {
        Character character = new Character();
        PrintHeader(1, character);
        string command;

        do
        {
            command = GetCommand();

            switch (command.ToLower())
            {
                case "create":
                    Console.Clear();
                    PrintHeader(2, character);
                    ClassDetails = await CharacterFactory.ChooseClass(character, Classes);
                    CharacterFactory.UpdateCharacterWithClassData(character, ClassDetails);
                    Console.Clear();
                    PrintHeader(3, character);
                    RaceDetails = await CharacterFactory.ChooseRace(character, Races);
                    CharacterFactory.UpdateCharacterWithRaceData(character, RaceDetails);
                    Console.Clear();
                    PrintHeader(4, character);
                    CharacterFactory.ChooseName(character); 
                    Console.Clear();
                    PrintHeader(5, character);
                    CharacterFactory.AssignAbilityScores(character);
                    CharacterFactory.UpdateAbilityScores(character, RaceDetails);
                    Console.Clear();
                    PrintHeader(6, character);
                    break;

                case "about":
                    Console.Clear();
                    PrintHeader(7, character);
                    break;

                case "menu":
                    Console.Clear();
                    PrintHeader(1, character);
                    break;

                case "print":
                    Console.Clear();
                    PrintCharacterData(character);
                    var printResponse = new Markup("[bold red3]There you go! Your character! In all of its relative glory. Use the menu options to navigate from here, or quit (if you're a quitter).[/]");
                    AnsiConsole.Write(PanelBlueprint(printResponse, "GREAT SUCCESS!"));
                    break;

                case "save":
                    SaveCharacterData(character);
                    var savedResponse = new Markup("[bold red3]Your character data has been saved to your desktop! Use the menu options to navigate from here, or quit (if you're a quitter).[/]");
                    AnsiConsole.Write(PanelBlueprint(savedResponse, "GREAT SUCCESS!"));
                    break;
            }
            
        } while (command.ToLower() != "quit");
    }
    
    private string GetCommand()
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                var farewell = new Markup("[red3 bold italic]Goodbye, traveller, it sure has been an experience. Tip your local web developer, hail Shar, et c.[/]");
                var byeByePanel = PanelBlueprint(farewell, "HERE ENDETH THE TALE");
                AnsiConsole.Write(byeByePanel);
                Environment.Exit(0);
            }

            if (!Commands.Contains(input.ToLower()))
            {
                var error = new Markup(
                    $"[red3 bold]{input}[/] is not a thing you can do. Choose something else, please. Or quit. Don't be difficult, [bold]I'm too old for this shit[/].");
                var errorPanel = PanelBlueprint(error, "NO CAN DO, BUCKAROO");
                AnsiConsole.Write(errorPanel);
                continue;
            }
            
            return input;
        }
    }
    
    public int[] RollForAbilityScores()
    {
        int[] abilityScores = new int[6];
        Random random = new Random();

        for (int i = 0; i < 6; i++)
        {
            int[] rolls = new int[4];
            for (int j = 0; j < 4; j++)
            {
                rolls[j] = random.Next(1, 7);
            }
            Array.Sort(rolls);
            abilityScores[i] = rolls[0] + rolls[1] + rolls[2];
        }

        return abilityScores;
    }

    private void PrintHeader(int option, Character character)
    {
        switch (option)
        {
            case 1:
                AnsiConsole.Write(
                    new FigletText("Character Creator v.1.0.0")
                        .Centered()
                        .Color(Color.Red));
                
                Thread.Sleep(2000);
                
                var greeting = new Markup("[red3 italic]Welcome, traveller, to the [bold]DnD5e character creator[/]! Here you can create a basic outline for your next DnD adventure. Or not. You could just quit. But then, why did you come here in the first place? Sit, sit. Have a hot beverage. Make some choices. [bold]And may the dice rolls be ever in your favor.[/][/]");

                var greetingPanel = PanelBlueprint(greeting, "HERE BE DRAGONS");
                AnsiConsole.Write(greetingPanel);
                
                AnsiConsole.Write(PrintShortMenu());
                break;
            
            case 2:
                AnsiConsole.Write(
                    new FigletText("Choose a class")
                        .Centered()
                        .Color(Color.Red));
                var classPrompt = new Markup("Are you a brawny, burly [bold darkred italic]barbarian[/]? A scrawny, rat-faced [bold gold3 italic]rogue[/]? Maybe you like to spend time frolicking through the fields in wildshape--we all know what you [bold springgreen4 italic]druids[/] get up to when you think no one is watching.\n    Either way, you must [bold red3 underline]choose a class[/].");

                var infoPanel = PanelBlueprint(classPrompt, "IS THE CLASS HALF FULL OR HALF EMPTY?");
                AnsiConsole.Write(infoPanel);
                Thread.Sleep(6000);

                StringBuilder sb =
                    new StringBuilder("[red3 bold]Choose one of the following available classes. I promise I won't judge.[/]\n \n");
                
                for (int i = 0; i < Classes.Count; i++)
                {
                    var dashes = new string('-', i+1);
                    if (i == Classes.Count-1)
                    {
                        sb.Append($"{dashes} {Classes[i]}");
                    }
                    else
                    {
                        sb.Append($"{dashes} {Classes[i]}\n");
                    }
                }

                var choices = new Markup(sb.ToString());
                var choicePanel = PanelBlueprint(choices, "NO PRESSURE, BUT YOU GOTTA CHOOSE");
                AnsiConsole.Write(choicePanel);
                break;
            
            case 3:
                AnsiConsole.Write(
                    new FigletText("Choose a race")
                        .Centered()
                        .Color(Color.Red));
                var racePrompt = new Markup(
                    $"A [red3 bold]{character.Class.ToLower()}[/], eh? Interesting. I knew a [red3 bold]{character.Class.ToLower()}[/] once. They weren't very nice. Anyway, you're going to have to [red3 bold italic]choose a race[/], too. I know, I know, we just got started and there are already too many choices. That's the way it works in DnD, I didn't write the rules. You can take it up with Gary.");
                var racePanel = PanelBlueprint(racePrompt, $"{character.Class.ToUpper()}, REALLY?");
                AnsiConsole.Write(racePanel);
                Thread.Sleep(6000);
                
                StringBuilder stringBuilder =
                    new StringBuilder("[bold red3]Choose one of the following available races. You do you. Again, I'm just giving you options, I won't judge... openly.[/]\n \n");
                
                for (int i = 0; i < Races.Count; i++)
                {
                    var dashes = new string('-', i+1);
                    if (i == Races.Count-1)
                    {
                        stringBuilder.Append($"{dashes} {Races[i]}");
                    }
                    else
                    {
                        stringBuilder.Append($"{dashes} {Races[i]}\n");
                    }
                }

                var raceChoices = new Markup(stringBuilder.ToString());
                var raceChoicePanel = PanelBlueprint(raceChoices, "OOOOH, RACY");
                AnsiConsole.Write(raceChoicePanel);
                break;
            
            case 4:
                AnsiConsole.Write(
                    new FigletText("Name thyself")
                        .Centered()
                        .Color(Color.Red));
                var namePrompt = new Markup(
                    $"A strapping(?), brave(?) [bold red3]{character.Race.ToLower()} {character.Class.ToLower()}[/] like you needs a good name. Let me suggest one for you!");
                var namePanel = PanelBlueprint(namePrompt, $"A {character.Class.ToUpper()} BY ANY OTHER NAME...");
                AnsiConsole.Write(namePanel);
                Thread.Sleep(2000);
                break;
            
            case 5:
                AnsiConsole.Write(
                    new FigletText("Let the good stats roll")
                        .Centered()
                        .Color(Color.Red));
                var rollText = new Markup(
                    $"Time to roll for your stats! For the sake of brevity, I will roll four D6 dice for each ability ([bold]strength, dexterity, constitution, intelligence, wisdom and charisma[/]), remove the worst roll, and the sum of the three remaining rolls will be up for assignment! Enough of me yapping, let's roll, [bold red3]{character.Name}[/]!");
                var rollPanel = PanelBlueprint(rollText, $"ROLL, YOU MAGNIFICENT {character.Race.ToUpper()}, ROLL");
                AnsiConsole.Write(rollPanel);
                Thread.Sleep(6000);
                break;
            
            case 6:
                AnsiConsole.Write(new FigletText("The End").Centered().Color(Color.Red));
                Thread.Sleep(2000);
                var endMessage = new Markup($"You have reached the end of the Character Creator--for now.\n \nYou can grab the specifics of your character, either by copying and pasting them from the console, or by saving your character in a text file on your desktop.\n    It includes many of the basics which are dependent on your chosen race([bold red3]{character.Race}[/]) and class([bold red3]{character.Class}[/]), such as [bold]starting items[/], [bold]proficiencies[/], [bold]languages[/] your character knows, and more.\n    You can bring this to your session 0 as an aid. Your DM will be so impressed!\n \nOr, if you're not satisfied with your choices, why not start over? It's your choice. Live a little!");
                Thread.Sleep(3000);
                var endPanel = PanelBlueprint(endMessage, $"HAIL {character.Name.ToUpper()}, THE {character.Class.ToUpper()} {character.Race.ToUpper()}");
                AnsiConsole.Write(endPanel);
                AnsiConsole.Write(PrintFullMenu());
                break;
            
            case 7:
                AnsiConsole.Write(
                    new FigletText("About")
                        .Centered()
                        .Color(Color.Red));
               Thread.Sleep(1000);
                var about = new Markup("[red3 italic]This is [bold]v.1.0.O[/] of this DnD5e console application character creator. It was created for a school assignment, because I wanted to try my hand at fetching data from an API in C#.\n \n   After working on it for a few weeks I realized that maybe I had bit off more than I could realistically chew. For one, character creation in DnD5e can be pretty complex, and I ran out of time for implementing all of the logic for updating the character data, such as offering the user to choose x number of items from a dynamic array dependent on class and/or race, or offering the player the chance to buy items instead (since their starting gold would depend on their chosen class and/or background)... and so on.\n \n    What's more, we haven't had the opportunity to learn about working with APIs in C# yet (as of the time of this deployment), and while fetching simple data proved relatively painless, more complex JSON response structures proved to be a bit of a challenge. My current solutions work, but I'm aware they are very far from ideal.\n \n    I'm looking forward to returning to this little project later, when I have the time and the opportunity, and continuing to work on making this a fully functional character creator, no DM required. For now, though, it is what it is. You get some basic info, you can save it to your desktop in a readable format and bring it to your DM, and they will (hopefully) appreciate the fact that you came to session 0 well prepared.\n \n    It's dangerous to go alone. Take this very basic character generator.\n \n   [bold]-- livoszlak@github[/][/]");
                
                var aboutPanel = PanelBlueprint(about, "VEM ÄR DU, VEM ÄR JAG, LEVANDE CHARADER");
                AnsiConsole.Write(aboutPanel);
                AnsiConsole.Write(PrintShortMenu());
                break;
        }
        
    }

    public Panel PanelBlueprint(Markup markup, string header)
    {
        return new Panel(markup)
        {
            Header = new PanelHeader(header, Justify.Center),
            Border = BoxBorder.Double,
            Padding = new Padding(2, 2, 2, 2),
            Expand = true
        };
    }

    private Panel PrintShortMenu()
    {
        var menuSb = new StringBuilder();
                
        for (int i = 0; i < 3; i++)
        {
            if (i == 2)
            {
                menuSb.Append($"{Commands[i].ToUpper()}");
            }
            else
            {
                menuSb.Append($"{Commands[i].ToUpper()}\n");
            }
        }
        var menu = new Markup(menuSb.ToString());
                
        var menuPanel = PanelBlueprint(menu, "MENÜ");
        return menuPanel;
    }

    private Panel PrintFullMenu()
    {
        var menuSb = new StringBuilder();
                
        for (int i = 0; i < Commands.Count; i++)
        {
            if (i == Commands.Count-1)
            {
                menuSb.Append($"{Commands[i].ToUpper()}");
            }
            else
            {
                menuSb.Append($"{Commands[i].ToUpper()}\n");
            }
        }
        var menu = new Markup(menuSb.ToString());
                
        var menuPanel = PanelBlueprint(menu, "MENÜ");
        return menuPanel;
    }
    
    private void PrintCharacterData(Character character)
    {
        Console.WriteLine($"Name: {character.Name}");
        Console.WriteLine($"Race: {character.Race}");
        Console.WriteLine($"Class: {character.Class}");
        
        Console.WriteLine("Ability Scores:");
        foreach (var score in character.AbilityScores)
        {
            Console.WriteLine($"\t{score.Key}: {score.Value}");
        }
        
        Console.WriteLine("Ability Bonuses:");
        foreach (var bonus in character.AbilityBonuses)
        {
            Console.WriteLine($"\t{bonus.Key}: {bonus.Value}");
        }
        
        Console.WriteLine($"Alignment: {character.Alignment}");
        Console.WriteLine($"Speed: {character.Speed}");
        Console.WriteLine($"Age: {character.Age}");
        Console.WriteLine($"Size: {character.Size}");
        
        Console.WriteLine("Languages:");
        foreach (var language in character.Languages)
        {
            Console.WriteLine($"\t{language}");
        }
        
        Console.WriteLine($"Language Description: {character.LanguageDescription}");
        
        Console.WriteLine("Traits:");
        foreach (var trait in character.Traits)
        {
            Console.WriteLine($"\t{trait}");
        }
        
        Console.WriteLine($"Hit Die: {character.HitDie}");
        
        Console.WriteLine("Proficiencies:");
        foreach (var proficiency in character.Proficiencies)
        {
            Console.WriteLine($"\t{proficiency}");
        }
        
        Console.WriteLine("Saving Throws:");
        foreach (var savingThrow in character.SavingThrows)
        {
            Console.WriteLine($"\t{savingThrow}");
        }
        
        Console.WriteLine("Starting Equipment:");
        foreach (var equipment in character.StartingEquipment)
        {
            Console.WriteLine($"\t{equipment}");
        }
        
    }

    private void SaveCharacterData(Character character)
    {
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var filePath = Path.Combine(desktopPath, $"{character.Name}_CharacterData.txt");

        var characterData = new StringBuilder();
        
        characterData.AppendLine($"Name: {character.Name}");
        characterData.AppendLine($"Race: {character.Race}");
        characterData.AppendLine($"Class: {character.Class}");
        
        characterData.AppendLine($"Ability Scores:");
        foreach (var score in character.AbilityScores)
        {
            characterData.AppendLine($"\t{score.Key}: {score.Value}");
        }
        
        characterData.AppendLine("Ability Bonuses:");
        foreach (var bonus in character.AbilityBonuses)
        {
            characterData.AppendLine($"\t{bonus.Key}: {bonus.Value}");
        }
        
        characterData.AppendLine($"Alignment: {character.Alignment}");
        characterData.AppendLine($"Speed: {character.Speed}");
        characterData.AppendLine($"Age: {character.Age}");
        characterData.AppendLine($"Size: {character.Size}");
        
        characterData.AppendLine("Languages:");
        foreach (var language in character.Languages)
        {
            characterData.AppendLine($"\t{language}");
        }
        
        characterData.AppendLine($"Language Description: {character.LanguageDescription}");
        
        characterData.AppendLine("Traits:");
        foreach (var trait in character.Traits)
        {
            characterData.AppendLine($"\t{trait}");
        }
        
        characterData.AppendLine($"Hit Die: {character.HitDie}");
        
        characterData.AppendLine("Proficiencies:");
        foreach (var proficiency in character.Proficiencies)
        {
            characterData.AppendLine($"\t{proficiency}");
        }
        
        characterData.AppendLine("Saving Throws:");
        foreach (var savingThrow in character.SavingThrows)
        {
            characterData.AppendLine($"\t{savingThrow}");
        }
        
        characterData.AppendLine("Starting Equipment:");
        foreach (var equipment in character.StartingEquipment)
        {
            characterData.AppendLine($"\t{equipment}");
        }

        File.WriteAllText(filePath, characterData.ToString());
    }
    
    // For testing purposes
    public void PrintRaceDetails(RaceDataModel raceDetails)
    {
        string json = JsonConvert.SerializeObject(raceDetails, Formatting.Indented);
        Console.WriteLine(json);
    }
}