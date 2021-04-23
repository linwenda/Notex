using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Commands;
using Funzone.Application.Configuration.Data;
using Funzone.Application.Configuration.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;

namespace Funzone.Application.Configuration.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        private readonly ITransactionContext _context;

        public TransactionBehaviour(
            ILogger logger,
            ITransactionContext context)
        {
            _logger = logger;
            _context = context;
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
                if (_context.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = await _context.BeginTransactionAsync())
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.Information("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
                            transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.Information("----- Commit transaction {TransactionId} for {CommandName}",
                            transaction.TransactionId, typeName);

                        await _context.CommitTransactionAsync(transaction);
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