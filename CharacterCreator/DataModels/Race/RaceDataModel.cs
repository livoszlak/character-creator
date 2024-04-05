namespace CharacterCreator.DataModels;
using System.Collections.Generic;
using Newtonsoft.Json;

public class RaceDataModel
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("speed")]
    public int Speed { get; set; }

    [JsonProperty("ability_bonuses")]
    public List<AbilityBonus> AbilityBonuses { get; set; }

    [JsonProperty("alignment")]
    public string Alignment { get; set; }

    [JsonProperty("age")]
    public string Age { get; set; }

    [JsonProperty("size")]
    public string Size { get; set; }

    [JsonProperty("size_description")]
    public string SizeDescription { get; set; }

    [JsonProperty("starting_proficiencies")]
    public List<Proficiency> StartingProficiencies { get; set; }

    [JsonProperty("starting_proficiency_options")]
    public StartingProficiencyOptions StartingProficiencyOptions { get; set; }

    [JsonProperty("languages")]
    public List<Proficiency> Languages { get; set; }

    [JsonProperty("language_desc")]
    public string LanguageDescription { get; set; }

    [JsonProperty("traits")]
    public List<Proficiency> Traits { get; set; }
}
public class AbilityBonus
{
    [JsonProperty("ability_score")]
    public AbilityScore AbilityScore { get; set; }

    [JsonProperty("bonus")]
    public int Bonus { get; set; }
}

public class AbilityScore
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class Proficiency
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class OptionItem
{
    [JsonProperty("option_type")]
    public string OptionType { get; set; }

    [JsonProperty("item")]
    public Proficiency Item { get; set; }
}

public class StartingProficiencyOptions
{
    [JsonProperty("desc")]
    public string Description { get; set; }

    [JsonProperty("choose")]
    public int Choose { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("from")]
    public From From { get; set; }
}

public class From
{
    [JsonProperty("option_set_type")]
    public string OptionSetType { get; set; }

    [JsonProperty("options")]
    public List<OptionItem> Options { get; set; }
}
