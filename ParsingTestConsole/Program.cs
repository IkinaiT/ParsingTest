using DataBase;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using ParsingTest.Models;
using System.Diagnostics;

DataBaseContext context;

var optionsPostgresSQL = new DbContextOptionsBuilder<DataBaseContext>();
FlurlClient client = new("https://line03w.bk6bba-resources.com");

optionsPostgresSQL.UseNpgsql("Host=localhost;Username=postgres;Password=admin;Database=parsedData;Pooling=True");

context = new(optionsPostgresSQL.Options);

var result = await client.Request("events", "listBase")
                .AppendQueryParam("lang", "ru")
                .AppendQueryParam("scopeMarket", "1600")
                .GetAsync()
                .ReceiveJson<Result>();

long lastVersion = 0;

SaveToDb(result);

while (true)
{
    Stopwatch sw = Stopwatch.StartNew();

    var tempResult = await client.Request("events", "list")
                .AppendQueryParam("lang", "ru")
                .AppendQueryParam("scopeMarket", "1600")
                .AppendQueryParam("version", lastVersion)
                .GetAsync()
                .ReceiveJson<Result>();

    lastVersion = tempResult.PacketVersion;

    if (tempResult.LiveEventInfos.Count > 0 || tempResult.CustomFactors.Count > 0)
    {
        Console.WriteLine($"Live events: {tempResult.LiveEventInfos.Count}");

        Console.WriteLine($"Factors: {tempResult.CustomFactors.Count}");

        Console.WriteLine($"Sports: {tempResult.Sports.Where(_ => _.Kind == "sport").Count()}");

        SaveToDb(tempResult);

        Console.WriteLine("Saved to db");

        sw.Stop();

        Console.WriteLine($"Total time: {sw.ElapsedMilliseconds}ms");
    }
}

void SaveToDb(Result result)
{
    foreach (var ev in result.Events)
    {
        var tempEvent = context.Events.FirstOrDefault(_ => _.Id == ev.Id);
        if (tempEvent != null)
        {
            if (tempEvent != ev)
                tempEvent = ev;
        }
        else
        {
            context.Events.Add(ev);
        }
    }

    foreach (var sport in result.Sports)
    {
        var tempSport = context.Sports.FirstOrDefault(_ => _.Id == sport.Id);
        if (tempSport != null)
        {
            if (tempSport != sport)
                tempSport = sport;
        }
        else
        {
            context.Sports.Add(sport);
        }
    }

    context.SaveChanges();
}