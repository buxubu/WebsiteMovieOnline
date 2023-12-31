global using System.Text.Json.Serialization;
global using WebMovieOnline.Models;
global using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebMovieOnline.Services.IMovie;
using WebMovieOnline.Services.IVideo;
using WebMovieOnline.Services.IGenres;
using WebMovieOnline.Services.ICountry;
using WebMovieOnline.Services.IActors;
using WebMovieOnline.Services.IMovieCasts;
using WebMovieOnline.Services.IAward;
using WebMovieOnline.Services.IActorsOfAward;
using WebMovieOnline.Services.IProductionCompany;
using WebMovieOnline.Services.IDirectors;
using WebMovieOnline.Services.SMTPEmail;
using WebMovieOnline.Services.ICategories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddCors(o => o.AddPolicy(name: "WebsiteMovie",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbwebsiteMovieOnlineContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IMovies, RepoMovies>();
builder.Services.AddScoped<IVideos, RepoVideos>();
builder.Services.AddScoped<IGenres, RepoGenres>();
builder.Services.AddScoped<ICountries, RepoCoutries>();
builder.Services.AddScoped<IActors, RepoActors>();
builder.Services.AddScoped<IMovieCasts, RepoMovieCasts>();
builder.Services.AddScoped<IAward, RepoAward>();
builder.Services.AddScoped<IActorsOfAward, RepoActorsOfAward>();
builder.Services.AddScoped<IProductionCompanies, RepoProductionCompanies>();
builder.Services.AddScoped<IDirectors, RepoDirectors>();
builder.Services.AddScoped<ISMTPEmail, RepoSMTPEmail>();
builder.Services.AddScoped<ICategories, RepoCategories>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
/*builder.Services.AddMvc();
builder.Services.AddControllers();*/


/*builder.Services.AddCors(o => o.AddPolicy(name: "WebsiteMovieOnline", policy =>
{
    policy.WithOrigins("https://localhost:7284/").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
}));*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "UploadImages")),
    RequestPath = "/UploadImages"
});

 app.UseStaticFiles(new StaticFileOptions
 {
     FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "UploadVideo")),
     RequestPath = "/UploadVideo"
 });

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "UploadVideoTrailer")),
    RequestPath = "/UploadVideoTrailer"
});
app.UseHttpsRedirection();

app.UseAuthorization();

/*app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());*/
app.UseCors("WebsiteMovie");

app.UseAuthentication();

/*app.UseEndpoints(enpoints =>
{
    enpoints.MapControllers();
});*/

app.MapControllers();

app.Run();
