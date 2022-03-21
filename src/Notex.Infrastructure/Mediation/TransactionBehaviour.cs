using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Notex.Core.Configuration;
using Notex.Core.Extensions;
using Notex.Infrastructure.EntityFrameworkCore;
using Notex.Messages;
using Serilog.Context;

namespace Notex.Infrastructure.Mediation;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
    private readonly NotexDbContext _dbContext;

    public TransactionBehaviour(
        NotexDbContext dbContext,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (_dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dbContext.BeginTransactionAsync())
                {
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
                            transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}",
                            transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);
                    }
                }
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}