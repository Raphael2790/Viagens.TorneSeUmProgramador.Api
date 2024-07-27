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

var ofertas = new List<OfertasDto>()
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
    ),
    new
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
    ),
    new
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
    ),
    new
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
    ),
    // Novos Dados Gerados
    new
    (
        "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Japão",
        "Tóquio, JP",
        "35.689487",
        "139.691711",
        "R$ 1800,00",
        "R$ 2500,00",
        "Mar - Abr 2024",
        "PassagemAerea"
    ),
    new
    (
        "https://images.unsplash.com/photo-1516166324702-0a3b56a30a0f?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "França",
        "Paris, FR",
        "48.856613",
        "2.352222",
        "R$ 2000,00",
        "R$ 2800,00",
        "Abr - Mai 2024",
        "Hospedagem"
    ),
    new
    (
        "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Austrália",
        "Sydney, AU",
        "-33.868820",
        "151.209290",
        "R$ 2500,00",
        "R$ 3000,00",
        "Mai - Jun 2024",
        "PassagemTerrestre"
    ),
    new
    (
        "https://images.unsplash.com/photo-1516166324702-0a3b56a30a0f?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Itália",
        "Roma, IT",
        "41.902783",
        "12.496366",
        "R$ 1700,00",
        "R$ 2300,00",
        "Jun - Jul 2024",
        "Completo"
    ),
    new
    (
        "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Espanha",
        "Barcelona, ES",
        "41.385064",
        "2.173404",
        "R$ 1600,00",
        "R$ 2200,00",
        "Jul - Ago 2024",
        "PassagemAerea"
    ),
    new
    (
        "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Alemanha",
        "Berlim, DE",
        "52.520007",
        "13.404954",
        "R$ 1500,00",
        "R$ 2100,00",
        "Ago - Set 2024",
        "Hospedagem"
    ),
    new
    (
        "https://images.unsplash.com/photo-1516166324702-0a3b56a30a0f?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Suíça",
        "Zurique, CH",
        "47.376886",
        "8.541694",
        "R$ 2200,00",
        "R$ 2800,00",
        "Set - Out 2024",
        "PassagemTerrestre"
    ),
    new
    (
        "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "México",
        "Cidade do México, MX",
        "19.432608",
        "-99.133209",
        "R$ 1400,00",
        "R$ 2000,00",
        "Out - Nov 2024",
        "Completo"
    ),
    new
    (
        "https://images.unsplash.com/photo-1516166324702-0a3b56a30a0f?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Grécia",
        "Atenas, GR",
        "37.983810",
        "23.727539",
        "R$ 1800,00",
        "R$ 2400,00",
        "Nov - Dez 2024",
        "Hospedagem"
    ),
    new
    (
        "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Tailândia",
        "Bangkok, TH",
        "13.756331",
        "100.501762",
        "R$ 1900,00",
        "R$ 2500,00",
        "Dez 2024 - Jan 2025",
        "PassagemAerea"
    ),
    new
    (
        "https://images.unsplash.com/photo-1516166324702-0a3b56a30a0f?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Holanda",
        "Amsterdã, NL",
        "52.367573",
        "4.904139",
        "R$ 1700,00",
        "R$ 2300,00",
        "Jan - Fev 2025",
        "PassagemTerrestre"
    )
};

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
    return ofertas.Skip(0).Take(10);
})
.WithName("ObterOfertas")
.WithOpenApi();

app.MapGet("viagens/ofertas-paginadas", (int pagina = 1, int itensPorPagina = 10, string tipoOferta = "") =>
{
    //var ofertasPaginadas = ofertas.Where(x => x.TipoPacote == tipoOferta).Skip((pagina - 1) * itensPorPagina).Take(itensPorPagina).ToList();
    var ofertasPaginadas = ofertas.Where(x => x.TipoPacote == tipoOferta).ToList();

    return ofertasPaginadas;
})
.WithName("ObterOfertasPaginadas")
.WithOpenApi();

app.MapGet("viagens/ofertas/{id:int}", (int id) =>
{
    var detalheOferta = new DetalheOfertaViagem(
        id,
        "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "Nova York",
        "Nova York, EUA",
        "40.712776",
        "-74.005974",
        "R$ 1500,00",
        "R$ 2000,00",
        "Fev - Mar 2024",
        "PassagemAerea",
        "Uma viagem incrível para Nova York com voos diretos e hospedagem em hotel 4 estrelas.",
        ["Passagem Aérea", "Hospedagem", "Café da Manhã"],
        ["Almoço", "Jantar", "Passeios Turísticos"],
        ["01/02/2024", "15/02/2024", "01/03/2024"],
        "Cancelamento gratuito até 7 dias antes da viagem.",
        [
            new ("João Silva", 5, "Viagem perfeita, tudo conforme o esperado!"),
            new ("Maria Oliveira", 4, "Ótima viagem, mas o hotel poderia ser melhor.")
        ],
        "support@travelagency.com",
        ["Cartão de Crédito", "PayPal", "Boleto Bancário"],
        [
            "https://images.unsplash.com/photo-1490644658840-3f2e3f8c5625?q=80&w=1974&auto=format&fit=crop",
            "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0?q=80&w=1974&auto=format&fit=crop"
        ],
        [
            "https://www.youtube.com/watch?v=example1",
            "https://www.youtube.com/watch?v=example2"
        ],
        [
            "Dia 1: Chegada e check-in no hotel",
            "Dia 2: Passeio pela cidade",
            "Dia 3: Visita a pontos turísticos",
            "Dia 4: Tempo livre",
            "Dia 5: Retorno"
        ],
        "Seguro de viagem disponível por R$ 100,00"
    );

    return detalheOferta;
})
    .WithName("OfertaDetalhe")
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
public class DetalheOfertaViagem(int id,string imagemUrl, string titulo, string descricao, string latitude, string longitude, string precoAtual, string precoOriginal, string periodo, string tipoPacote, string descricaoDetalhada, List<string> inclusoes, List<string> exclusoes, List<string> datasDisponiveis, string politicaCancelamento, List<AvaliacaoDto> avaliacoes, string informacoesContato, List<string> opcoesPagamento, List<string> imagens, List<string> videos, List<string> itinerario, string seguroViagem)
{
    public int Id { get; set; } = id;
    public string ImagemUrl { get; set; } = imagemUrl;
    public string Titulo { get; set; } = titulo;
    public string Descricao { get; set; } = descricao;
    public string Latitude { get; set; } = latitude;
    public string Longitude { get; set; } = longitude;
    public string PrecoAtual { get; set; } = precoAtual;
    public string PrecoOriginal { get; set; } = precoOriginal;
    public string Periodo { get; set; } = periodo;
    public string TipoPacote { get; set; } = tipoPacote;
    public string DescricaoDetalhada { get; set; } = descricaoDetalhada;
    public List<string> Inclusoes { get; set; } = inclusoes;
    public List<string> Exclusoes { get; set; } = exclusoes;
    public List<string> DatasDisponiveis { get; set; } = datasDisponiveis;
    public string PoliticaCancelamento { get; set; } = politicaCancelamento;
    public List<AvaliacaoDto> Avaliacoes { get; set; } = avaliacoes;
    public string InformacoesContato { get; set; } = informacoesContato;
    public List<string> OpcoesPagamento { get; set; } = opcoesPagamento;
    public List<string> Imagens { get; set; } = imagens;
    public List<string> Videos { get; set; } = videos;
    public List<string> Itinerario { get; set; } = itinerario;
    public string SeguroViagem { get; set; } = seguroViagem;
}

public record AvaliacaoDto(string Usuario, int Nota, string Comentario);

public record OfertasDto(string? Imagem, string? Titulo, string? Local, string? Latitude, string? Longitude, string? Preco, string? PrecoAnterior, string Data, string TipoPacote);
public record Usuario(int Id, string Email, string Senha);