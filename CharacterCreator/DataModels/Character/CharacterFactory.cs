using System.Text;
using CharacterCreator.Services;
using Spectre.Console;

namespace CharacterCreator.DataModels;
public class CharacterFactory
{
    private static readonly List < string > RandomNameSuggestions = new List < string > ()
    {
        "Grandalf Flimsypants",
        "Frowndo Ooglebongs",
        "Tyler Burden",
        "Tanstaafl the Peckish",
        "Periwinkle Poppy Pecker-Punisher",
        "Bargain-Bin Baggins",
        "Dumbleberry Bunglebeard",
        "Bilgebro Sack-o-crumpets",
        "Kitten Eversmirk",
        "Apropos of Nothing",
        "Dobbie Dorkins",
        "Legolame Longlegs",
        "Thornberry Oakencheeks",
        "Haggle the Hapless",
        "Voldepork",
        "McFrillypants the Second",
        "Last-and-Certainly-Least, Mighty Pirate",
        "Stumpkin"
    };
  private static string FormatName(string? input)
  {
      if (input == null)
      {
          return string.Empty;
      }
      
      if (input.Contains(' '))
      {
          string[] nameParts = input.Split(' ');
          
          for (int i = 0; i < nameParts.Length; i++)
          {
              nameParts[i] = char.ToUpper(nameParts[i][0]) + nameParts[i].Substring(1).ToLower();
          }
          input = string.Join(" ", nameParts);
      }
      else
      {
          input = char.ToUpper(input[0]) + input.Substring(1).ToLower();
      }
      
      return input;
  }
  
  private static Application.CharacterCreator _characterCreator = new Application.CharacterCreator();
  
  public static async Task<ClassDataModel> ChooseClass(Character character, List < string > classes)
  {
      bool isValidInput = false;
      var wrongInputCount = 0;
      do
      {
          string chosenClass = Console.ReadLine();

          isValidInput = classes.Any(c => c.Equals(chosenClass, StringComparison.CurrentCultureIgnoreCase));
          
          if (!isValidInput)
          {
              switch (wrongInputCount)
              {
                  case 0:
                      AnsiConsole.WriteLine();
                      AnsiConsole.Write(new Markup($"... [red3][bold]{chosenClass}[/] is not a class. Try again.[/]\n"));
                      break;
                  
                  case 1:
                      AnsiConsole.WriteLine();
                      AnsiConsole.Write(new Markup($"[red3][bold]{chosenClass}[/] is not a class either. Come on now.[/]\n"));
                      break;
                  
                  case 2:
                      AnsiConsole.WriteLine();
                      AnsiConsole.Write(new Markup("I see [bold steelblue1]Oghma[/] was otherwise preoccupied on the day of your creation.\n"));
                      Thread.Sleep(2000);
                      AnsiConsole.Write(new Markup("    We'll try to remedy that [italic bold red3]horrifically[/] low [bold]INT[/] stat later.\n"));
                      Thread.Sleep(2000);
                      AnsiConsole.Write(new Markup("        But for now, please [bold red3]LOOK AT THE LIST[/] and [bold red3]CHOOSE A CLASS FROM THAT LIST.[/]\n"));
                      Thread.Sleep(2000);
                      AnsiConsole.Write(new Markup("Here it is again. Read [bold italic]carefully[/] and choose.\n \n"));
                      Thread.Sleep(2000);
                      Console.Clear();
                      
                      StringBuilder sb =
                        new StringBuilder("[bold][red3]Choose one of the following available classes. However, at this point, I [underline]might[/] actually judge. I'm sure you understand. Well, actually, no, I'm not sure you understand anything at all. However, I am nothing if not charitable. [italic]One last chance[/]. Here you go.[/][/]\n \n");
                      
                      for (int i = 0; i < classes.Count; i++)
                      {
                          var dashes = new string('-', i+1);
                          if (i == classes.Count-1)
                          {
                              sb.Append($"{dashes} {classes[i]}");
                          }
                          else
                          {
                              sb.Append($"{dashes} {classes[i]}\n");
                          }
                      }
                      
                      var choices = new Markup(sb.ToString());

                      AnsiConsole.Write(_characterCreator.PanelBlueprint(choices, "UNBELIEVABLE. CONSIDER MY MIND PROPERLY BOGGLED."));
                      break;
                }
              
                wrongInputCount++;
                
          }
          else
          {
              character.Class = char.ToUpper(chosenClass[0]) + chosenClass.Substring(1).ToLower();
          }
          
          if (wrongInputCount > 3)
          {
                Random random = new Random();
                int classIndex = random.Next(0, classes.Count());
                AnsiConsole.WriteLine();
                
                AnsiConsole.Write(new Markup("After three consecutive failures to choose from the list--[italic]fairly unbelievable as far as failures go[/]--I will just give you one at random.\n"));
                Thread.Sleep(2000);
                AnsiConsole.Write(new Markup($"    Your class is [bold magenta3_1]{classes[classIndex].ToLower()}[/].\n"));
                Thread.Sleep(2000);
                AnsiConsole.Write(new Markup("        You're welcome.\n \n"));
                Thread.Sleep(2000);
                AnsiConsole.Write(new Markup("            Now, let's move on before my bones turn to dust.\n"));
                Thread.Sleep(3000);
                
                character.Class = classes[classIndex];
                isValidInput = true;
          }
          
      } while (!isValidInput);
      
      var classDetails = await ClassService.GetClassDetails(character);
      return classDetails;
  }

  public static async Task<RaceDataModel> ChooseRace(Character character, List < string > races)
  {
      bool isValidInput = false;
      var wrongInputCount = 0;

      do
      {
          string chosenRace = Console.ReadLine();
          isValidInput = races.Any(c => c.Equals(chosenRace, StringComparison.CurrentCultureIgnoreCase));
          
          if (!isValidInput)
          {
              switch (wrongInputCount)
              {
                  case 0:
                      AnsiConsole.WriteLine();
                      AnsiConsole.Write(new Markup($"[bold red3]{chosenRace}[/] is not a race. [bold]Read the list. Pick one. From said list.[/]\n"));
                      break;
                  
                  case 1:
                      AnsiConsole.WriteLine();
                      AnsiConsole.Write(new Markup($"You're never going to believe this, but [bold red3]{chosenRace}[/] is not a race either.\n"));
                      Thread.Sleep(1000);
                      AnsiConsole.Write(
                          new Markup("    Are you three drunk kobolds stacked on top of each other in an ill-fitting cloak,"));
                      AnsiConsole.Write(new Markup(" or are you just incredibly, [bold]infuriatingly[/], [italic bold]infernally[/] dumb?\n \n"));
                      Thread.Sleep(2000);
                      AnsiConsole.Write(new Markup("Please, for the love of [purple3 bold]Chronos[/], just pick a race before the flat circle of time curls into a ball and perishes.\n"));
                      break;
                  
                  case 2:
                      AnsiConsole.WriteLine();
                      AnsiConsole.Write(new Markup($"I don’t know why I thought a [red3 bold]{character.Class.ToLower()}[/] would know better than to pull a stunt like this. Joke’s on me. Ha ha.\n"));
                      Thread.Sleep(2000);
                      AnsiConsole.Write(new Markup("    We'll try to remedy that [italic red3]abysmally[/] low [bold]INT[/] stat later, although you are starting to seem like a lost cause.\n \n"));
                      Thread.Sleep(2000);
                      AnsiConsole.Write(new Markup("But for now, please [red3 bold]LOOK AT THE LIST[/] and [red3 bold]CHOOSE A RACE FROM THAT LIST[/].\n"));
                      Thread.Sleep(2000);
                      AnsiConsole.Write(new Markup("[bold]Here it is. [red3]Again.[/] Read carefully and choose.[/]\n"));
                      AnsiConsole.WriteLine();
                      Thread.Sleep(2000);
          
                      StringBuilder stringBuilder =
                          new StringBuilder("[bold red3]Here are your options. These are the available races. That means these are the races you can choose between. This is where you choose one of the races listed below, type that race name, and press [silver]ENTER[/]. Clear?[/]\n \n");
                            
                      for (int i = 0; i < races.Count; i++)
                      {
                          var dashes = new string('-', i+1);
                          
                          if (i == races.Count-1)
                          {
                              stringBuilder.Append($"{dashes} {races[i]}");
                          }
                          else
                          {
                              stringBuilder.Append($"{dashes} {races[i]}\n");
                          }
                          
                      }

                        var raceChoices = new Markup(stringBuilder.ToString());
                        AnsiConsole.Write(_characterCreator.PanelBlueprint(raceChoices, "HERE BE YOUR OPTIONS. THE OPTIONS. THE OPTIONS OF YOUR CHOICE."));
                      break;
                  
              }
              wrongInputCount++;
              
          }
          else
          {
              character.Race = char.ToUpper(chosenRace[0]) + chosenRace.Substring(1).ToLower();
          }
          
          if (wrongInputCount > 3)
          {
              Random random = new Random();
              int raceIndex = random.Next(0, races.Count());
              AnsiConsole.WriteLine();
              AnsiConsole.Write(new Markup("You know what, I gave you several tries to get this right. But now, I'm choosing for you.\n"));
              Thread.Sleep(1000);
              AnsiConsole.Write(new Markup($"    Your race is [bold violet]{races[raceIndex].ToLower()}[/]. You seem like a {races[raceIndex].ToLower()}.\n"));
              Thread.Sleep(1000);
              AnsiConsole.Write(new Markup("    ...\n"));
              Thread.Sleep(2000);
              AnsiConsole.Write(new Markup("        No, I don't mean that in a racist way. Not like-- I mean-- [bold]shit[/].\n"));
              Thread.Sleep(2000);
              AnsiConsole.Write(new Markup("        .....\n"));
              Thread.Sleep(2000);
              AnsiConsole.Write(new Markup("            You're... welcome.\n"));
              Thread.Sleep(1000);
              AnsiConsole.Write(new Markup("                [italic](Why am I doing this to myself? I’m not even getting paid.)[/]\n"));
              Thread.Sleep(1000);
              AnsiConsole.Write(new Markup("            .......\n \n"));
              Thread.Sleep(2000);
              AnsiConsole.Write(new Markup("[bold]Let's just move on.[/]\n"));
              Thread.Sleep(3000);
              
              character.Race = races[raceIndex];
              isValidInput = true;
              
          }
          
      } while (!isValidInput);
      
      var raceDetails = await RaceService.GetRaceDetails(character);
      return raceDetails;
  }
  public static void ChooseName(Character character)
  {
      bool isValidInput = false;
      bool userChoseOwnName = false;
      Random random = new Random();
      string randomName = RandomNameSuggestions[random.Next(RandomNameSuggestions.Count)];
      
      do
      {
          var nameSuggestion = new Markup($"How about... [bold red3]{randomName}[/]! How does that sound? Neat, am I right? \n \n1 - Yes, [bold red3]{randomName}[/] really captures my essence! \n2 - Not really feeling it, give me another suggestion. \n3 - No, your suggestions are awful, let me choose on my own.");
      
          AnsiConsole.Write(_characterCreator.PanelBlueprint(nameSuggestion, $"A MIGHTY NAME FOR A MIGHTY {character.Race.ToUpper()}"));

          AnsiConsole.WriteLine();
          string choice = Console.ReadLine();
          
          switch (choice)
          {
              case "1":
                character.Name = randomName;
                isValidInput = true;
                break;
              
              case "2":
                randomName = RandomNameSuggestions[random.Next(RandomNameSuggestions.Count)];
                break;
              
              case "3":
                AnsiConsole.Write(new Markup("Wow, that's... unnecessarily hurtful. But fine.\n  [bold red3]Adventurer, name thyself[/] [italic](if you think you can do better)[/]:\n"));
                string input = Console.ReadLine();
                string chosenName = FormatName(input);
                
                if (chosenName.Length > 0) {
                  character.Name = chosenName;
                  userChoseOwnName = true;
                  isValidInput = true;
                }
                break;
              
              default:
                AnsiConsole.Write(new Markup("There are only [bold red3]3 choices[/]... they are [bold]right there[/]. And yet... you managed to mess this up. Try again.\n \n"));
                randomName = RandomNameSuggestions[random.Next(RandomNameSuggestions.Count)];
                Thread.Sleep(2000);
                break;
          }
          
      } while (!isValidInput);
      
      if (userChoseOwnName)
      {
          AnsiConsole.Write(new Markup($"Okay, [bold red3]{character.Name}[/] is your name. I feel like I could have given you a better one, but sure, I guess [bold]{character.Name}[/] works fine for a [bold]{character.Class.ToLower()}[/]. Let's move on."));
          Thread.Sleep(4000);
      }
      else
      {
          AnsiConsole.Write(new Markup($"[bold red3]{character.Name}[/] is an incredible name for a {character.Class.ToLower()}, if I do say so myself. One of my finest. Let's move on!"));
          Thread.Sleep(3000);
      }
    
  }
  public static void AssignAbilityScores(Character character)
    {
        int[] abilityScores = _characterCreator.RollForAbilityScores();
        for (int i = 0; i < abilityScores.Length; i++)
        {
            AnsiConsole.Write(new Markup($"[bold steelblue1_1]ROLL {i + 1}:[/] {abilityScores[i]}\n"));
            Thread.Sleep(1000);
        }

        bool[] assignedRolls = new bool[abilityScores.Length];

        for (int i = 0; i < abilityScores.Length; i++)
        {
            string abilityKey = character.AbilityScores.Keys.ElementAt(i);
            int statNumber;

            do
            {
                AnsiConsole.Write(new Markup($"Enter the number of the roll you want to assign to [bold steelblue1_1]{abilityKey}[/]: "));
                bool isNumber = int.TryParse(Console.ReadLine(), out statNumber);
                
                if (!isNumber || statNumber < 1 || statNumber > 6 || assignedRolls[statNumber - 1])
                {
                    AnsiConsole.WriteLine();
                    AnsiConsole.Write(new Markup("[bold]Invalid input.[/] Please enter a number between 1 and 6 that hasn't been chosen before. You can do it. I believe at you.\n \n"));
                    statNumber = 0;
                }
                
            } while (statNumber < 1 || statNumber > 6 || assignedRolls[statNumber - 1]);

            assignedRolls[statNumber - 1] = true;

            switch (statNumber)
            {
                case 1:
                    character.AbilityScores["STR"] = abilityScores[i];
                    break;
                case 2:
                    character.AbilityScores["DEX"] = abilityScores[i];
                    break;
                case 3:
                    character.AbilityScores["CON"] = abilityScores[i];
                    break;
                case 4:
                    character.AbilityScores["INT"] = abilityScores[i];
                    break;
                case 5:
                    character.AbilityScores["WIS"] = abilityScores[i];
                    break;
                case 6:
                    character.AbilityScores["CHA"] = abilityScores[i];
                    break;
            }
        }
        
    }
    
  public static void UpdateAbilityScores(Character character, RaceDataModel raceData)
    {
        var abilityBonuses = raceData.AbilityBonuses
            .Where(bonus => character.AbilityScores.ContainsKey(bonus.AbilityScore.Name.ToUpper()))
            .ToList();

        foreach (var abilityBonus in abilityBonuses)
        {
            string abilityScoreName = abilityBonus.AbilityScore.Name.ToUpper();
            character.AbilityScores[abilityScoreName] += abilityBonus.Bonus;
            character.AbilityBonuses[abilityScoreName] = abilityBonus.Bonus;
        }
        
        var strB = new StringBuilder($"Excellent! After applying your ability bonuses from choosing [bold red3]{character.Race}[/] as your race, your stats are as follows:\n \n");
        foreach (var stat in character.AbilityScores)
        {
            strB.Append($"[bold steelblue1_1]{stat.Key}:[/] {stat.Value}\n");
        }
        var statMessage = new Markup(strB.ToString());
        AnsiConsole.Write(_characterCreator.PanelBlueprint(statMessage, $"SPLENDIFEROUS STATS, {character.Name.ToUpper()}!"));
        Thread.Sleep(4000);
    }

    public static void UpdateCharacterWithRaceData(Character character, RaceDataModel raceData)
    {
        character.Alignment = raceData.Alignment;
        character.Speed = raceData.Speed;
        character.Age = raceData.Age;
        character.Size = raceData.SizeDescription;
        
        foreach (var language in raceData.Languages)
        {
            character.Languages.Add(language.Name);
        }
        foreach (var trait in raceData.Traits)
        {
            character.Traits.Add(trait.Name);
        }
        character.LanguageDescription = raceData.LanguageDescription;
    }

    public static void UpdateCharacterWithClassData(Character character, ClassDataModel classData)
    {
        character.HitDie = $"d{classData.HitDie}";
        foreach (var proficiency in classData.Proficiencies)
        {
            character.Proficiencies.Add(proficiency.Name);
        }
        foreach (var savingThrow in classData.SavingThrows)
        {
            character.SavingThrows.Add(savingThrow.Name);
        }
        foreach (var equipment in classData.StartingEquipment)
        {
            character.StartingEquipment.Add(equipment.Equipment.Name);
        }
    }
    
}