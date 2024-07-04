namespace OOP_ICT.Third.Tests;

using Xunit;
using OOP_ICT.Third.Models;
using OOP_ICT.Third.Services;
using System;
using Newtonsoft.Json;
using System.IO;

public class SaveServiceTests
{
    private readonly string tempFilePath = Path.Combine(Path.GetTempPath(), "test_blackjack_casino.json");
    private PlayerGeneratorService playersGenerator = new PlayerGeneratorService();

    public SaveServiceTests()
    {
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }
    }

    [Fact]
    public void DeserializeFromJson_ValidJson_ReturnsObject()
    {
        // Arrange
        var json = JsonConvert.SerializeObject(new BlackJackCasinoL3("TestCasino", 1000));
        // Act
        var result = SaveService.DeserializeFromJson(json);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("TestCasino", result.Name); // Предполагаем, что в классе есть свойство Name
        Assert.Equal(1000, result.Balance);
    }

    [Fact]
    public void DeserializeFromJson_ValidJson_ReturnsObject_GameStarted()
    {
        var casino = new BlackJackCasinoL3("TestCasino", 1000);
        var players = playersGenerator.GeneratePlayers(10);
        casino.Start(players);
        var json = JsonConvert.SerializeObject(casino);
        var result = SaveService.DeserializeFromJson(json);
        Assert.NotNull(result);
        Assert.Equal(casino.Name, result.Name); 
        Assert.Equal(casino.Balance, result.Balance);
        for (int i = 0; i < casino.CurrentPlayers.Count; i++)
        {
            Assert.Equal(casino.CurrentPlayers[i], result.CurrentPlayers[i]);
        }

        Assert.NotNull(result.Dealer);
    }

    [Fact]
    public void DeserializeFromJson_InvalidJson_ThrowsApplicationException()
    {
        var json = "invalid json";
        Assert.Throws<ApplicationException>(() => SaveService.DeserializeFromJson(json));
    }

    [Fact]
    public void SaveToFile_ValidData_SavesFile()
    {
        var casino = new BlackJackCasinoL3("TestCasino", 1000);
        SaveService.SaveToFile(casino, tempFilePath);
        Assert.True(File.Exists(tempFilePath));
        string content = File.ReadAllText(tempFilePath);
        Assert.Contains("TestCasino", content);
        Assert.Contains("1000", content);
    }

    [Fact]
    public void LoadFromFile_ValidFile_ReturnsObject()
    {
        var casino = new BlackJackCasinoL3("TestCasino", 1000);
        File.WriteAllText(tempFilePath, JsonConvert.SerializeObject(casino));
        var result = SaveService.LoadFromFile(tempFilePath);
        Assert.NotNull(result);
        Assert.Equal("TestCasino", result.Name);
    }

    [Fact]
    public void LoadFromFile_InvalidFile_ThrowsApplicationException()
    {
        var filePath = Path.Combine(Path.GetTempPath(), "nonexistent.json");
        Assert.Throws<ApplicationException>(() => SaveService.LoadFromFile(filePath));
    }
    
    ~SaveServiceTests()
    {
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }
    }
}