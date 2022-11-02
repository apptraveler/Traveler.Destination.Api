using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Traveler.Destination.Api.Infra.CrossCutting.Environments.Configurations;
using Microsoft.Data.Sqlite;

namespace Traveler.Destination.Api.Application.QueryHandlers;

public abstract class QueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IDbConnection _dbConnection;

    protected QueryHandler(ApplicationConfiguration applicationConfiguration)
    {
        _dbConnection = new SqliteConnection(applicationConfiguration.ConnectionString);
    }

    protected IDbConnection GetDatabaseConnection()
    {
        if (_dbConnection.State == ConnectionState.Closed)
        {
            _dbConnection.Open();
        }

        return _dbConnection;
    }

    protected void CloseDatabaseConnection()
    {
        if (_dbConnection.State is ConnectionState.Open or ConnectionState.Broken)
        {
            _dbConnection.Close();
        }
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
