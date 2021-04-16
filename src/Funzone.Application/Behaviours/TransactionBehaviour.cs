using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Commands;
using Funzone.Application.Configuration.Data;
using Funzone.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;

namespace Funzone.Application.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        private readonly IFunzoneDbContext _dbContext;

        public TransactionBehaviour(
            ILogger logger,
            IFunzoneDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!typeof(ICommand).IsAssignableFrom(typeof(TRequest)) &&
                !typeof(ICommand<TResponse>).IsAssignableFrom(typeof(TRequest)))
            {
                return await next();
            }
            
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
}