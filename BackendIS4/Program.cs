var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MigrationsAssembly = typeof(Program).Assembly.GetName().Name;
var ConnectionString = "Host=isabelle.db.elephantsql.com;Port=5432;Database=yezkrefj;User Id=yezkrefj;Password=kiLj5HdtfNdyK2dp9t1o4LmgbI6t7p6T";
#if DEBUG
{
    ConnectionString = "Host=localhost;Port=5432;Database=identity;User Id=postgres;Password=teste";
}
#endif

builder.Services.AddDbContext<IS4DbContext>(opt => opt.UseNpgsql(ConnectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IS4DbContext>()
    .AddDefaultTokenProviders();

var m_IdentityServerBuilder = builder.Services.AddIdentityServer();

#if DEBUG
{
    m_IdentityServerBuilder.CMX_ConfigurarRELEASE(ConnectionString, MigrationsAssembly!);
    //m_IdentityServerBuilder.CMX_ConfigurarDEBUG();
}
#else
{
    m_IdentityServerBuilder.CMX_ConfigurarRELEASE();
}
#endif

var m_JWK = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "tempkey.jwk"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_JWK)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddScoped<IAuthorizationHandler, IsAdmHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdm", builder => builder.AddRequirements(new IsAdmRequirement()));
});

var app = builder.Build();

app.UseCors(opt => opt
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.MigrationExtensionAsync();

#if !DEBUG
{
    await app.MigrationExtensionAsync();
}
#endif

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseIdentityServer();

app.Run();
