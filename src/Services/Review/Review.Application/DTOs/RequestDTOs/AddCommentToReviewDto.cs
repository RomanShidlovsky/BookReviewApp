using Review.Domain.Entities;

namespace Review.Application.DTOs.RequestDTOs;

public sealed record AddCommentToReviewDto(
    string ReviewId,
    Comment Comment);