using Blog.Application.DTOs.Posts;
using MediatR;

namespace Blog.Application.Features.Base.CreatePost;

public abstract record BaseCreatePostCommand(Guid UserId, CreatePostDto CreatePostDto) : IRequest<Guid>;