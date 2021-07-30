﻿using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Application.Configuration.Responses;
using MediatR;
using MarchNote.Domain;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Application.Configuration.Behaviors
{
    public class ResponseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : IMarchNoteResponse
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (ValidationException ex)
            {
                var builder = new StringBuilder();
                foreach (var (key, value) in ex.Errors)
                {
                    builder.Append($"{key}：{string.Join('.', value)}\n ");
                }

                return CreateResponse(DefaultResponseCode.Invalid, builder.ToString());
            }
            catch (ForbiddenException ex)
            {
                return CreateResponse(DefaultResponseCode.Forbidden, ex.Message);
            }
            catch (NotFoundException ex)
            {
                return CreateResponse(DefaultResponseCode.NotFound, ex.Message);
            }
            catch (BusinessException ex)
            {
                return CreateResponse((int)ex.Code, ex.Message);
            }
        }

        private static TResponse CreateResponse(int code, string message)
        {
            var response = Activator.CreateInstance<TResponse>();

            response.Code = code;
            response.Message = message;

            return response;
        }
    }
}