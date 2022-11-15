using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Application.Schedulers;

public class SeedDestinationsJob : Scheduler
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly IUnitOfWork _uow;

    protected override bool ExecuteOnce => true;

    public SeedDestinationsJob(IServiceProvider serviceProvider, ILogger<SeedDestinationsJob> logger) : base(logger)
    {
        var scope = serviceProvider.CreateScope();

        _destinationRepository = scope.ServiceProvider.GetRequiredService<IDestinationRepository>();
        _uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    public override string GetScheduleName()
    {
        return nameof(SeedDestinationsJob);
    }

    public override DateTime GetStartTime()
    {
        return DateTime.UtcNow;
    }

    public override TimeSpan GetInterval()
    {
        return TimeSpan.Zero;
    }

    public override async Task Job()
    {
        try
        {
            var destinations = await _destinationRepository.GetAll();
            var destinationsToInsert = GenerateData();

            if (destinations is null || !destinations.Any())
            {
                foreach (var destination in destinationsToInsert)
                {
                    _destinationRepository.Add(destination);
                }

                await _uow.CommitAsync();

                return;
            }

            var destinationRemainingToInsert = destinationsToInsert.Where(x => !destinations.Any(c => c.Name.Equals(x.Name, StringComparison.InvariantCulture)));

            foreach (var destination in destinationRemainingToInsert)
            {
                _destinationRepository.Add(destination);
            }

            await _uow.CommitAsync();
        }
        catch (Exception e)
        {
            Logger.LogCritical("Erro ao salvar os destinos #### Exception: {0} ####", e.ToString());
        }
    }

    private static IEnumerable<Destination> GenerateData()
    {
        var canadaImages = new List<DestinationImage>
        {
            new("https://cdn.pixabay.com/photo/2020/04/21/02/32/buildings-5070537_960_720.jpg"),
            new("https://cdn.pixabay.com/photo/2020/10/31/18/29/mountains-5701889_960_720.jpg"),
            new("https://images.unsplash.com/photo-1507992781348-310259076fe0?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
        };

        var japanImages = new List<DestinationImage>
        {
            new("https://cdn.pixabay.com/photo/2020/04/21/02/32/buildings-5070537_960_720.jpg"),
            new("https://cdn.pixabay.com/photo/2020/10/31/18/29/mountains-5701889_960_720.jpg"),
            new("https://images.unsplash.com/photo-1558862107-d49ef2a04d72?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
        };

        var reykjavikImages = new List<DestinationImage>
        {
            new("https://images.unsplash.com/photo-1602167352652-52dba4750482?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
            new("https://images.unsplash.com/photo-1583279956846-653caa8417ed?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
            new("https://images.unsplash.com/photo-1474690870753-1b92efa1f2d8?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
        };

        var edinburghImages = new List<DestinationImage>
        {
            new("https://images.unsplash.com/photo-1535448033526-c0e85c9e6968?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
            new("https://images.unsplash.com/photo-1557425769-de134747b1f6?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
            new("https://images.unsplash.com/photo-1567802474769-eb1d9ebc9416?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1175&q=80"),
            new("https://images.unsplash.com/photo-1581095664605-ef68bfde1867?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1074&q=80"),
        };

        var canadaTags = new[] {DestinationTags.Beach, DestinationTags.Trails, DestinationTags.Waterfalls, DestinationTags.Mountains};
        var japanTags = new[] {DestinationTags.HistoricalPlaces, DestinationTags.Trails};
        var reykjavikTags = new[] {DestinationTags.Waterfalls, DestinationTags.Mountains, DestinationTags.Beach};
        var edinburghTags = new[] {DestinationTags.HistoricalPlaces, DestinationTags.TouristSpots, DestinationTags.Mountains};

        var canadaCoordinates = new[] {new RouteCoordinates("", "", 43.651070, -79.347015)};
        var japanCoordinates = new[] {new RouteCoordinates("", "", 35.6894, 139.692)};
        var reykjavikCoordinates = new[] {new RouteCoordinates("", "", 64.128288, -21.827774)};
        var edinburghCoordinates = new[] {new RouteCoordinates("", "", 55.953251, -3.188267)};

        return new[]
        {
            new Destination(
                "Toronto - Canada",
                "Toronto",
                "Canada",
                "Toronto, a capital da província de Ontário, é uma grande cidade canadense localizada ao longo da costa noroeste do Lago Ontário. Ela é uma metrópole dinâmica com um centro de arranha-céus imponentes, todos ofuscados pela famosa Torre CN. Toronto também tem muitos espaços verdes, incluindo desde o oval Queen’s Park até o High Park, com 400 acres de área, além de trilhas, instalações esportivas e um jardim zoológico.",
                DestinationAverageSpend.Medium,
                new DestinationClimateAverage(-11, 2),
                canadaCoordinates,
                canadaImages,
                canadaTags
            ),
            new Destination(
                "Kyoto - Japão",
                "Kyoto",
                "Japão",
                "O Japão, país insular no Oceano Pacífico, tem cidades densas, palácios imperiais, parques nacionais montanhosos e milhares de santuários e templos. Os trens-bala Shinkansen conectam as principais ilhas: Kyushu (com as praias subtropicais de Okinawa), Honshu (onde ficam Tóquio e a sede do memorial da bomba atômica de Hiroshima) e Hokkaido (famosa como destino para a prática de esqui). Tóquio, a capital, é conhecida por seus arranha-céus e lojas e pela cultura pop.",
                DestinationAverageSpend.High,
                new DestinationClimateAverage(2, 22),
                japanCoordinates,
                japanImages,
                japanTags
            ),
            new Destination(
                "Reykjavik - Islândia",
                "Reykajvik",
                "Islândia",
                "Reykjavik, no litoral da Islândia, é a capital e a maior cidade do país. Ela abriga os museus nacional e Saga, que contam a história viking da Islândia. A incrível igreja em concreto Hallgrimskirkja e a cúpula em vidro giratória Perlan oferecem uma vista abrangente do mar e das montanhas próximas. Exemplificando a atividade vulcânica da ilha está a Lagoa Azul hidromineral geotérmica, próxima do vilarejo de Grindavik.",
                DestinationAverageSpend.Medium,
                new DestinationClimateAverage(-15, -4),
                reykjavikCoordinates,
                reykjavikImages,
                reykjavikTags
            ),
            new Destination(
                "Edinburgo - Escócia",
                "Edinburgo",
                "Escócia",
                "Edimburgo é a compacta e elevada capital da Escócia, com uma cidade velha medieval e uma elegante cidade nova georgiana que inclui jardins e construções neoclássicas. Em um ponto mais elevado da cidade, fica o Castelo de Edimburgo, que abriga as joias da coroa da Escócia e a Pedra do Destino, usada na coroação dos governantes escoceses. Arthur’s Seat é um pico imponente com vistas panorâmicas em Holyrood Park, e a colina Calton Hill é coberta por monumentos e memoriais.",
                DestinationAverageSpend.High,
                new DestinationClimateAverage(-14, 5),
                edinburghCoordinates,
                edinburghImages,
                edinburghTags
            ),
        };
    }
}
