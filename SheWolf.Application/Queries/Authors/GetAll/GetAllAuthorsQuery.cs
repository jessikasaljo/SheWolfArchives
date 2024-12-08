using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;

namespace SheWolf.Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQuery : IRequest<OperationResult<List<AuthorDto>>>
    {

    }
}
