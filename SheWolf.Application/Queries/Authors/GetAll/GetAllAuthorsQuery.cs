using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQuery : IRequest<OperationResult<List<Author>>>
    {

    }
}
