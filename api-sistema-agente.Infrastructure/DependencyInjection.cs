    services.AddScoped<IMailService, MailService>();
    services.AddScoped<IPasswordEncryption, PasswordEncryption>();

    services.Configure<SecretsApi>(configuration.GetSection("SecretsApi"));
    services.AddSingleton<ISecretsService, SecretsService>();

    var serviceProvider = services.BuildServiceProvider();
    var secretsOptions = serviceProvider.GetRequiredService<IOptions<SecretsApi>>().Value;
