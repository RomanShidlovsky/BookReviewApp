using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.Review.Commands.AddComment;

public sealed record AddCommentToReviewCommand(AddCommentToReviewDto Dto) : ICommand; 
