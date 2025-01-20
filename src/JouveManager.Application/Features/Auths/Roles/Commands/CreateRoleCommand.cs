using System;
using JouveManager.Application.CQRS;
using JouveManager.Application.Models.Authorization;

namespace JouveManager.Application.Features.Auths.Roles.Commands;

public class CreateRoleCommand : ICommand<Role>
{
    public required string Name { get; set; }
}