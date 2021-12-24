using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Extensions;
using SmartNote.Infrastructure.EntityFrameworkCore;

namespace SmartNote.Infrastructure.Mediator;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly SmartNoteDbContext _dbContext;
    private readonly ILogger _logger;

    public TransactionBehaviour(SmartNoteDbContext dbContext,
        ILogger logger)
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
                using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                {
                    _logger.Information("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
                        transaction.TransactionId, typeName, request);

                    response = await next();

                    _logger.Information("----- Commit transaction {TransactionId} for {CommandName}",
                        transaction.TransactionId, typeName);

                    await _dbContext.CommitTransactionAsync(transaction);
                }
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}