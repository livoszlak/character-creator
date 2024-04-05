using System.Text;

namespace CharacterCreator.DataModels;

public class Character
{
    public string Name { get; set; }
    public string Race { get; set; }
    public string Class { get; set; }
    public Dictionary<string, int> AbilityScores { get; }
    public Dictionary<string, int> AbilityBonuses { get; }
    public string Alignment { get; set; }
    public int Speed { get; set; }
    public string Age { get; set; }
    public string Size { get; set; }
    public List<string> Languages { get; }
    public string LanguageDescription { get; set; }
    public List<string> Traits { get; }
    public string HitDie { get; set; }
    public List<string> Proficiencies { get; }
    public List<string> SavingThrows { get; }
    public List<string> StartingEquipment { get; }
    
    
    public Character()
    {
        AbilityScores = new Dictionary<string, int>()
        {
            { "STR", 0 },
            { "DEX", 0 },
            { "CON", 0 },
            { "INT", 0 },
            { "WIS", 0 },
            { "CHA", 0 }
        };
        AbilityBonuses = new Dictionary<string, int>();
        Languages = new List<string>();
        Traits = new List<string>();
        Proficiencies = new List<string>();
        SavingThrows = new List<string>();
        StartingEquipment = new List<string>();
    }
    
}