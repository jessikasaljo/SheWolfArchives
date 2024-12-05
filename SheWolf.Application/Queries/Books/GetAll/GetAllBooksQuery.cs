using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Books.GetAll
{
    public class GetAllBooksQuery : IRequest<OperationResult<List<Book>>>
    {

    }
}