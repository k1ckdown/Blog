using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Base.CreatePost;

public abstract record BaseCreatePostCommand(Guid UserId, CreatePostDto CreatePostDto) : IRequest<Guid>;