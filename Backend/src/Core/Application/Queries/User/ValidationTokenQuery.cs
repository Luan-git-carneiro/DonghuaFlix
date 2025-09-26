
using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Queries.User;

public class ValidationTokenQuery : IRequest<ValidationResult>
    {
        public string Token { get; }
        
        public ValidationTokenQuery(string token)
        {
            Token = token;
        }
    }