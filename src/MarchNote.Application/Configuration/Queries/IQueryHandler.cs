﻿using MediatR;

namespace MarchNote.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> :
        IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}