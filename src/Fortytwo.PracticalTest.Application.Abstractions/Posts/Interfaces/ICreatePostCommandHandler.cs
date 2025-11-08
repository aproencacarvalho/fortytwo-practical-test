using System;
using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Fortytwo.PracticalTest.Application.Abstractions.Posts.Models;

namespace Fortytwo.PracticalTest.Application.Abstractions.Posts.Interfaces;

public interface ICreatePostCommandHandler : IRequestHandler<CreatePostCommand, OperationResult<int>>;