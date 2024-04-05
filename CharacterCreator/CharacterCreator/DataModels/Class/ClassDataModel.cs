namespace CharacterCreator.DataModels;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ClassDataModel
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("hit_die")]
    public int HitDie { get; set; }

    [JsonProperty("proficiencies")]
    public List<Proficiency> Proficiencies { get; set; }

    [JsonProperty("saving_throws")]
    public List<SavingThrow> SavingThrows { get; set; }

    [JsonProperty("starting_equipment")]
    public List<StartingEquipment> StartingEquipment { get; set; }

    [JsonProperty("starting_equipment_options")]
    public List<StartingEquipmentOption> StartingEquipmentOptions { get; set; }
}

public class ClassProficiency
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class SavingThrow
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class StartingEquipment
{
    [JsonProperty("equipment")]
    public Equipment Equipment { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}

public class Equipment
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class StartingEquipmentOption
{
    [JsonProperty("desc")]
    public string Description { get; set; }

    [JsonProperty("choose")]
    public int Choose { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("from")]
    public ClassFrom ClassFrom { get; set; }
}

public class ClassFrom
{
    [JsonProperty("option_set_type")]
    public string OptionSetType { get; set; }

    [JsonProperty("options")]
    public List<Option> Options { get; set; }
}

public class Option
{
    [JsonProperty("option_type")]
    public string OptionType { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("of")]
    public Equipment Of { get; set; }

    [JsonProperty("items")]
    public List<Item> Items { get; set; }
}

public class Item
{
    [JsonProperty("option_type")]
    public string OptionType { get; set; }

    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("of")]
    public Equipment Of { get; set; }
}
