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

            var destinationRemainingToInsert = destinationsToInsert.Where(x =>
                !destinations.Any(c => c.Name.Equals(x.Name, StringComparison.InvariantCulture)));

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
            new(
                "https://images.unsplash.com/photo-1507992781348-310259076fe0?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
        };

        var japanImages = new List<DestinationImage>
        {
            new("https://cdn.pixabay.com/photo/2020/04/21/02/32/buildings-5070537_960_720.jpg"),
            new("https://cdn.pixabay.com/photo/2020/10/31/18/29/mountains-5701889_960_720.jpg"),
            new(
                "https://images.unsplash.com/photo-1558862107-d49ef2a04d72?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
        };

        var reykjavikImages = new List<DestinationImage>
        {
            new(
                "https://images.unsplash.com/photo-1602167352652-52dba4750482?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
            new(
                "https://images.unsplash.com/photo-1583279956846-653caa8417ed?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
            new(
                "https://images.unsplash.com/photo-1474690870753-1b92efa1f2d8?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
        };

        var edinburghImages = new List<DestinationImage>
        {
            new(
                "https://images.unsplash.com/photo-1535448033526-c0e85c9e6968?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
            new(
                "https://images.unsplash.com/photo-1557425769-de134747b1f6?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"),
            new(
                "https://images.unsplash.com/photo-1567802474769-eb1d9ebc9416?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1175&q=80"),
            new(
                "https://images.unsplash.com/photo-1581095664605-ef68bfde1867?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1074&q=80"),
        };

        var canadaTags = new[]
            { DestinationTags.Beach, DestinationTags.Trails, DestinationTags.Waterfalls, DestinationTags.Mountains };
        var japanTags = new[] { DestinationTags.HistoricalPlaces, DestinationTags.Trails };
        var reykjavikTags = new[] { DestinationTags.Waterfalls, DestinationTags.Mountains, DestinationTags.Beach };
        var edinburghTags = new[]
            { DestinationTags.HistoricalPlaces, DestinationTags.TouristSpots, DestinationTags.Mountains };

        var canadaCoordinates = new[]
        {
            new RouteCoordinates("Museu Real de Ontário (Royal Ontario Museum)",
                "Com mais de 100 anos de idade e um misto de arquitetura clássica e futurista, por si só esse museu já é uma atração turística. Não é um museu apenas de arte. Lá estão mais de 6 mil objetos relacionados a temas como cultura universal e história natural. Entre meteoritos, esqueletos e réplicas de dinossauros, é possível passar horas a fio admirando seu acervo.",
                43.66835253043088, -79.39505242295864),
            new RouteCoordinates("Toronto Islands",
                "A cerca de 20 minutos de balsa do centro de Toronto, o pequeno arquipélago de Toronto Islands tem diferentes atrações. Lá estão o parque de diversões Centreville Amusement Park, o histórico e mal-assombrado Farol de Gibraltar, algumas praias, jardins e uma loja de aluguel de bicicletas. E um dos jeitos mais legais de explorar o máximo das ilhas, é justamente pedalando. Sem falar que de lá se tem a melhor vista de Toronto.",
                43.623409, -79.368683),
            new RouteCoordinates("Ripley’s Aquarium of Canada",
                "Inaugurado em 2013, é o maior aquário do Canadá. Abriga mais de 15 mil animais marinhos de 450 espécies. Particularmente, prefiro não visitar aquários, por considerar uma forma de Turismo Cruel. Mas se você não liga pra isso, pode combinar com a vista à CN Tower, que ficam bem ao lado.",
                43.64230672857729, -79.38659777931329),
        };

        var japanCoordinates = new[]
        {
            new RouteCoordinates("Ginkaku-ji",
                "O Ginkaku-ji se situa na região leste de Quioto no pé do monte Tsukimachi, sua obra foi iniciada no final do século XV pelo neto do xogun Yoshimitsu. Yoshimasa Ashikaga que porém morreu pouco antes da obra estar completa. Foi transformada depois de sua morte em um templo budista por pedido no testamento.",
                35.02719699305962, 135.79821652682745),
            new RouteCoordinates("Yasaka Jinja",
                "Conhecido também como Santuário Gion, o Yasaka Jinja (八 坂 神社) é um dos santuários mais famosos de Kyoto. O local foi construído há mais de 1350 anos, fica localizado entre os distritos de Gion e de Higashiyama, sendo acessado por ambos.",
                35.00378055589729, 135.77849006401152),
            new RouteCoordinates("Kiyomizu-dera",
                "Conhecido como o “Templo da Água Pura” é um dos templos mais deslumbrantes e famosos de todo o Japão. A construção foi feita na Cachoeira Otowa em 789, localizada nas colinas arborizadas de Quioto.",
                34.99507580757543, 135.78502484032109),
        };

        var reykjavikCoordinates = new[]
        {
            new RouteCoordinates("Igreja Hallgrímskirkja",
                "Visitar a Hallgrímskirkja é um passeio obrigatório para quem vai à capital da Islândia. Com 74, 5 metros, esta igreja luterana é a maior do país, além de ser a construção mais alta de Reykjavik e a sexta maior estrutura em território islandês. Sua arquitetura é daquelas difíceis de se ver igual em outro lugar do mundo.Lembrando o estilo gótico, a Hallgrímskirkja possui colunas construídas como representação do basalto vulcânico.",
                64.14213306574827, -21.926583658930568),
            new RouteCoordinates("Solfar Sculpture",
                "Muitos a confundem com o esqueleto de um navio viking, e de fato, até parece. Mas a realidade é que a escultura de aço Solfar, ou Sun Voyager, é uma reverência ao sol, com simbolismo de luz e esperança. Solfar também faz parte dos cartões-postais de Reykjavik, Islândia. Está localizada na orla, de frente para o mar e com uma linda vista para o Monte Esja. Cenário lindíssimo para belas fotos e admirar as maravilhas naturais islandesas.",
                64.14774754209844, -21.922284601259758),
            new RouteCoordinates("Catedral Dómkirkjan",
                "Aproveite que está na praça para conhecer a Catedral de Reykjavik, a Dómkirkjan. Bem modesta se comparada com a Hallgrímskirkja, esta igreja possui um valor histórico enorme para o país. Além de ser a mais antiga e berço do luteranismo na Islândia, ela teve papel no reconhecimento do processo de independência islandês. Para conhecê-la, é só chegar: a catedral fica aberta diariamente e a entrada é gratuita. ",
                64.14672749611469, -21.939416224842372),
        };
        var edinburghCoordinates = new[]
        {
            new RouteCoordinates("Castelo de Edimburgo",
                "Sobre um rochedo basáltico de um vulcão extinto e no meio da capital escocesa encontramos o Castelo de Edimburgo, a atração mais visitada do país. É possível avistá-lo de vários ângulos em torno da cidade.",
                55.94872466042688, -3.199937657843652),
            new RouteCoordinates("Monumento Scott",
                "Ao caminhar pela Princes Street, rua paralela a Royal Mile, encontraremos o Monumento Scott. Na verdade, ele pode ser avistado de longe e sua beleza vitoriana gótica logo chama a atenção. Trata-se uma torre de 60m de altura e que homenageia um dos maiores escritores escoceses de todos os tempos: Sir Walter Scott. No meio do monumento, encontra-se uma estátua do escritor esculpida em mármore e que possui duas vezes o seu tamanho natural.",
                55.952477090556656, -3.1933062880146146),
            new RouteCoordinates("Calton Hill",
                "No topo da capital escocesa, nas imediações das duas principais vias da cidade, a Royal Mile e a Princes Street, está uma colina chamada Calton Hill. O local possui monumentos neoclássicos de inspiração grega, além de um observatório astronômico.",
                55.95512370049137, -3.182753304409358),
        };

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
