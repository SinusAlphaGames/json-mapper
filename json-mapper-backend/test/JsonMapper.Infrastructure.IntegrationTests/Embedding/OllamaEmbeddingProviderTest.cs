using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using JsonMapper.Application;
using JsonMapper.Application.Json;
using JsonMapper.Infrastructure.Embedding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonMapper.Infrastructure.IntegrationTests.Embedding;

[TestClass]
[TestSubject(typeof(OllamaEmbeddingProvider))]
public class OllamaEmbeddingProviderTest
{

    [TestMethod]
    public void Should_generate_embedding_from_ollama()
    {
        var services = new ServiceCollection();

        services.AddLogging();
        
        services.AddApplicationLayer();
        services.AddInfrastructureLayer();

        var serviceProvider = services.BuildServiceProvider();
        
        var embeddingProvider = serviceProvider.GetRequiredService<IEmbeddingProvider>();
        var embedding = embeddingProvider.GetEmbedding("siema", CancellationToken.None);
        // logger.LogInformation("Embedding: {Embedding}", embedding);
        Console.WriteLine("Embedding: " + embedding.Result);
    }
}