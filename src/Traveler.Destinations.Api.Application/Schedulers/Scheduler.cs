using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Traveler.Destinations.Api.Application.Schedulers;

public abstract class Scheduler : BackgroundService
{
    private readonly IDictionary<string, Timer> _schedules;
    protected readonly ILogger Logger;

    protected Scheduler(ILogger logger)
    {
        _schedules = new Dictionary<string, Timer>();
        Logger = logger;
    }

    protected virtual bool ExecuteImmediately => false;
    protected virtual bool ExecuteOnce => false;

    public async Task Schedule()
    {
        if (ExecuteImmediately) await Job();
        var firstRun = GetStartTime();
        var now = DateTime.Now;
        var timeToGo = firstRun.Subtract(now);
        if (timeToGo <= TimeSpan.Zero)
        {
            timeToGo = TimeSpan.Zero;
        }

        var timer = new Timer(_ => Job(), null, timeToGo, GetInterval());
        Logger.LogInformation($"O job {GetScheduleName()} foi agendado com sucesso para o horário: {firstRun}");
        _schedules.Add(GetScheduleName(), timer);
        if (ExecuteOnce)
        {
            await Job();
            await Stop();
        }
    }

    public async Task Stop()
    {
        var schedulerName = GetScheduleName();
        if (_schedules == null || !_schedules.ContainsKey(schedulerName)) return;
        await _schedules[schedulerName].DisposeAsync();
        _schedules.Remove(schedulerName);
        Logger.LogInformation($"O job {GetScheduleName()} foi removido com sucesso");
    }

    public abstract string GetScheduleName();
    public abstract DateTime GetStartTime();
    public abstract TimeSpan GetInterval();
    public abstract Task Job();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Schedule();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await Stop();
    }
}
