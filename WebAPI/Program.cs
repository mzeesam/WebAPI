var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



List<AfricanCity> cities = new List<AfricanCity>()
{
    new AfricanCity(){CityID=1, CityCode = "KLA", CityName = "Kampala"},
    new AfricanCity(){CityID=2, CityCode = "KLB", CityName = "Juba"},
    new AfricanCity(){CityID=3, CityCode = "KLC", CityName = "Nairobi"},
    new AfricanCity(){CityID=4, CityCode = "KLD", CityName = "Kigali"},
    new AfricanCity(){CityID=5, CityCode = "KLE", CityName = "Kinshasa"},
    new AfricanCity(){CityID=6, CityCode = "KLF", CityName = "Bujumbura"},
    new AfricanCity(){CityID=7, CityCode = "KLG", CityName = "Lusaka"},
};

app.MapGet("/cities", () => {
    //var cities = new List<string>() { "Juba", "Nairobi" };
    return Results.Ok(cities);
});
app.MapGet("/cities/{Id}", (int Id) => {
    var newcities = cities.FirstOrDefault(c => c.CityID == Id);
    return Results.Ok(newcities);
});
app.MapGet("/cities/name={name}&code={code}", (string name, string code) => { 
    var newcities= cities.Where(s => s.CityName ==name || s.CityCode == code).ToList();
    return Results.Ok(newcities);
});
app.MapPost("/cities", (AfricanCity city) =>
{
    cities.Add(city);
    Results.Created($"/cities/{city.CityID}", city);
});
app.MapPut("/cities", (int Id, AfricanCity city) => {
    var record = cities.Where(c => c.CityID == Id).FirstOrDefault();
    if (record is null)
        return Results.NotFound();
    record.CityCode = city.CityCode;
    record.CityName = city.CityName;
    return Results.NoContent();
});
app.MapDelete("/cities", (int Id) => {
    var record = cities.Where(c => c.CityID == Id).FirstOrDefault();
    if (record is null)
        return Results.NotFound();
    cities.Remove(record);
   return Results.NoContent();
});

app.Run();

internal class AfricanCity
{
    public int CityID { get; set; }
    public string CityCode { get; set; }
    public string CityName { get; set; }
}