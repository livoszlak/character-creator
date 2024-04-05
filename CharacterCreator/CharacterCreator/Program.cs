class Program
{
    static async Task Main(string[] args)
    {
        var characterCreator = new CharacterCreator.Application.CharacterCreator();
        await characterCreator.Run();
    }
}
