using System;
using System.Linq;
using Funzone.BuildingBlocks.Application;
using Microsoft.AspNetCore.Http;

namespace Funzone.PhotoAlbums.Api.Configuration.ExecutionContext
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                if (_httpContextAccessor
                    .HttpContext?
                    .User?
                    .Claims?
                    .SingleOrDefault(x => x.Type == "sub")?
                    .Value != null)
                {
                    return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.Single(
                        x => x.Type == "sub").Value);
                }

                //Testing
                return Guid.NewGuid();
            }
        }

        public Guid CorrelationId
        {
            get
            {
                if (_httpContextAccessor.HttpContext != null && IsAvailable &&
                    _httpContextAccessor.HttpContext.Request.Headers.Keys.Any(
                        x => x == CorrelationMiddleware.CorrelationHeaderKey))
                {
                    return Guid.Parse(
                        _httpContextAccessor.HttpContext.Request.Headers[CorrelationMiddleware.CorrelationHeaderKey]);
                }

                throw new ApplicationException("Http context and correlation id is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
    }
}