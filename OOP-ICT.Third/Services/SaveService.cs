using Newtonsoft.Json;
using OOP_ICT.Third.Models;

namespace OOP_ICT.Third.Services;

public static class SaveService
{
    public static BlackJackCasinoL3 DeserializeFromJson(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<BlackJackCasinoL3>(json) ?? throw new InvalidOperationException();
        }
        catch (JsonException ex)
        {
            throw new ApplicationException("An error occurred while deserializing the object from JSON.", ex);
        }
    }

    public static void SaveToFile(BlackJackCasinoL3 casino, string filePath)
    {
        try
        {
            string json = JsonConvert.SerializeObject(casino, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while saving the object to the file: {filePath}", ex);
        }
    }

    public static BlackJackCasinoL3 LoadFromFile(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<BlackJackCasinoL3>(json) ?? throw new InvalidOperationException();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while loading the object from the file: {filePath}", ex);
        }
    }

}