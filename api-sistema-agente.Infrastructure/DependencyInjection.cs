    services.AddScoped<IMailService, MailService>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IPasswordEncryption, PasswordEncryption>();

    services.Configure<SecretsApi>(configuration.GetSection("SecretsApi"));
    services.AddSingleton<ISecretsService, SecretsService>();

    var serviceProvider = services.BuildServiceProvider();
    var secretsOptions = serviceProvider.GetRequiredService<IOptions<SecretsApi>>().Value;

    services.AddAuthentication(x =>
    {
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
      x.RequireHttpsMetadata = false;
      x.SaveToken = true;
      x.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretsOptions.SecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
      };
    });
