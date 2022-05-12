using Musikbibliotek.Data;
using Musikbibliotek.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MusicDataContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IArtistRepository, ArtistRepositoryImpl>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepositoryImpl>();
builder.Services.AddScoped<ISongRepository, SongRepositoryImpl>();

builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<ISongService, SongService>();

var app = builder.Build();


/*

Create Service layer for all classes and mapp them to dtoResult.
Make sure that the mapping is correct so Artist returns list of albums,
Albums returns list of songs and Song returns its album and artist.

 */

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
