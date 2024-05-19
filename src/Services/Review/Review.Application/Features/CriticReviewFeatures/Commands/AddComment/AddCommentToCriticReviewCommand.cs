using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.CriticReviewFeatures.Commands.AddComment;

public sealed record AddCommentToCriticReviewCommand(AddCommentToReviewDto Dto) : ICommand; 
