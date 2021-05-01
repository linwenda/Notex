using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.Application.Configuration.Data;
using Funzone.Application.Configuration.Queries;

namespace Funzone.Application.Posts.Queries
{
    public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, PostDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPostByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql = @$"SELECT TOP (1) [Post].[Id] AS {nameof(PostDto.Id)}
                              ,[Post].[ZoneId] AS {nameof(PostDto.ZoneId)}
                              ,[Post].[AuthorId] AS {nameof(PostDto.AuthorId)}
                              ,[Post].[Title] AS {nameof(PostDto.Title)}
                              ,[Post].[Content] AS {nameof(PostDto.Content)}
                              ,[Post].[PostedTime] AS {nameof(PostDto.PostedTime)}
                              ,[Post].[EditedTime] AS {nameof(PostDto.EditedTime)}
                              ,[Post].[Type] AS {nameof(PostDto.Type)}
                              ,[Post].[Status] AS {nameof(PostDto.Status)}
                          FROM [Posts] AS [Post]
                          WHERE [Post].[Id] = @PostId";

            return await connection.QuerySingleOrDefaultAsync<PostDto>(sql, new {request.PostId});
        }
    }
}