using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Define um esquema securo para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Implementa a autenticação em todos os endpoints da API
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //define o emissor e a audiência validas para o token JWT obtidos da aplicação
            ValidAudience = "https://localhost:7066/",
            ValidIssuer = "https://localhost:7066/",
            //Define a chave de assinatura usada para assinar e verificar o token JWT.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ZdYM000OLlMQG6VVVp1OH7RxtuEfGvBnXarp7gHuw1qvUC5dcGt3SNM"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Viagens API V1");
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseSwagger();

app.MapGet("viagens/mais-buscadas", () =>
{
    var maisBuscados = new List<MaisBuscadosDto>()
            {
                new(
                    "https://images.unsplash.com/photo-1500916434205-0c77489c6cf7?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Nova York/USA",
                    "Distância - 2500 mi",
                    "40,712776",
                    "-74,005974"
                ),
                new
                (
                    "https://images.unsplash.com/photo-1518684079-3c830dcef090?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Dubai/Arabe Emirates",
                    "Distância - 2500 mi",
                    "25,0657000",
                    "55,1712800"
                ),new
                (
                    "https://images.unsplash.com/photo-1555992828-ca4dbe41d294?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Rome/Italy",
                    "Distância - 2500 mi",
                    "41,8919300",
                    "12,5113300"
                ),
                new
                (
                    "https://images.unsplash.com/photo-1549144511-f099e773c147?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Paris/France",
                    "Distância - 2500 mi",
                    "48,8566100",
                    "2,3514999"
                ),new
                (
                    "https://images.unsplash.com/photo-1570135460230-1407222b82a2?q=80&w=2032&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Madrid/Spain",
                    "Distância - 2500 mi",
                    "40,4165000",
                    "-3,7025600"
                )
            };
    return maisBuscados;
})
.WithName("ObterMaisBuscados")
.WithOpenApi();

app.MapGet("viagens/ofertas", () =>
{
    var maisBuscados = new List<OfertasDto>()
            {
               new
                (
                    "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Nova York",
                    "Nova York, EUA",
                    "40.712776",
                    "-74.005974",
                    "R$ 1500,00",
                    "R$ 2000,00",
                    "Fev - Mar 2024",
                    "PassagemAerea"
                ),
                new
                (
                    "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Africa",
                    "Nova Deli, AFR",
                    "40.712776",
                    "-74.005974",
                    "R$ 1500,00",
                    "R$ 2000,00",
                    "Fev - Mar 2024",
                    "Hospedagem"
                ),new
                (
                    "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Canadá",
                    "Van Couver, CAN",
                    "40.712776",
                    "-74.005974",
                    "R$ 1500,00",
                    "R$ 2000,00",
                    "Fev - Mar 2024",
                    "PassagemTerrestre"
                ),
                new
                (
                    "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "China",
                    "Hong Kong, CH",
                    "40.712776",
                    "-74.005974",
                    "R$ 1500,00",
                    "R$ 2000,00",
                    "Fev - Mar 2024",
                    "Completo"
                ),new
                (
                    "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Brasil",
                    "São Paulo, BR",
                    "40.712776",
                    "-74.005974",
                    "R$ 1500,00",
                    "R$ 2000,00",
                    "Fev - Mar 2024",
                    "PassagemAerea"
                ),new
                (
                    "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "Argentina",
                    "Buenos Aires, ARG",
                    "40.712776",
                    "-74.005974",
                    "R$ 1500,00",
                    "R$ 2000,00",
                    "Fev - Mar 2024",
                    "Hospedagem"
                )
            };
    return maisBuscados;
})
.WithName("ObterOfertas")
.WithOpenApi();

app.MapPost("usuarios/login", ([FromBody] Usuario usuario) =>
{
    var usuarioAtual = usuario;

    if (usuarioAtual is null)
    {
        return Results.NotFound("Usuário não encontrado");
    }

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ZdYM000OLlMQG6VVVp1OH7RxtuEfGvBnXarp7gHuw1qvUC5dcGt3SNM"));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
            new Claim(ClaimTypes.Email , usuario.Email)
    };

    var token = new JwtSecurityToken(
        issuer: "https://localhost:7066/",
        audience: "https://localhost:7066/",
        claims: claims,
        expires: DateTime.Now.AddDays(8),
        signingCredentials: credentials);

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    // Gera um refresh token e seria vnculado ao usuario
    var randomNumber = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(randomNumber);
    var refreshToken = Convert.ToBase64String(randomNumber);

    return Results.Ok(new
    {
        access_token = jwt,
        token_type = "Bearer",
        user_id = usuarioAtual.Id,
        user_name = usuarioAtual.Email,
        expiration = token.ValidTo,
        refresh_token = refreshToken
    });
})
.WithName("Login")
.WithOpenApi();

app.Run();

public record MaisBuscadosDto(string? Imagem, string? Titulo, string? Descricao, string? Latitude, string? Longitude);
public record OfertasDto(string? Imagem, string? Titulo, string? Local, string? Latitude, string? Longitude, string? Preco, string? PrecoAnterior, string Data, string TipoPacote);
public record Usuario(int Id, string Email, string Senha);