using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using JetBrains.Annotations;
using JsonMapper.Application.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonMapper.Application.Tests.Json;

[TestClass]
[TestSubject(typeof(JsonFlattener))]
public class JsonFlattenerTest
{
    [TestMethod]
    public void Flatten_Test()
    {
        var json = CreateTestJson();

        var jsonNode = JsonNode.Parse(json);
        var flattenedJson = JsonFlattener.Flatten(jsonNode);

        Assert.IsNotNull(flattenedJson);

        foreach (var fieldNode in flattenedJson)
        {
            Console.WriteLine(fieldNode.Path);    
        }
        
        Assert.AreEqual(13, flattenedJson.Count, "Expected flattened JSON object to have 13 properties");
    }

    private static string CreateTestJson()
    {
        var obj = new
        {
            id = 1,
            name = "Anna Kowalska",
            age = 28,
            email = "anna.kowalska@example.com",
            isActive = true,
            address = new
            {
                street = "ul. Kwiatowa 10",
                city = "Wroclaw",
                postalCode = "50-001",
                country = "Poland"
            },
            hobbies = new[] { "reading", "traveling", "programming" },
            createdAt = "2026-07-05T12:00:00Z"
        };
        var json = JsonSerializer.Serialize(obj);
        return json;
    }
}