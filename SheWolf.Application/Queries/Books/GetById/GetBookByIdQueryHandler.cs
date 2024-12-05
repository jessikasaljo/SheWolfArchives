using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Queries.Books.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<Book>.Failure("The request was null.");
            }

            Book wantedBook = await _bookRepository.GetBookById(request.Id);

            if (wantedBook == null)
            {
                return OperationResult<Book>.Failure($"Book with ID {request.Id} not found.");
            }

            return OperationResult<Book>.Successful(wantedBook);
        }
    }
}