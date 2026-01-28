using System;
using System.IO;
using System.Text.Json;
using AuthDesktop.Models;
using Services;

namespace AuthDesktop.UI.ServicesImpl;

public class ConfigurationService: IConfigurationService
{
    private const string ConfigFileName = "appsettings.json";

    public string AuthApiUrl { get; }

    public ConfigurationService()
    {
        if (!File.Exists(ConfigFileName))
            throw new FileNotFoundException(
                $"Config file '{ConfigFileName}' not found.");

        var json = File.ReadAllText(ConfigFileName);

        var config = JsonSerializer.Deserialize<AppConfiguration>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (string.IsNullOrWhiteSpace(config?.AuthApiUrl))
            throw new InvalidOperationException("AuthApiUrl not specified in config file.");

        AuthApiUrl = config.AuthApiUrl.TrimEnd('/');
    }
}