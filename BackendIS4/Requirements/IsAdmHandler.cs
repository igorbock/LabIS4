
namespace BackendIS4.Requirements;

public class IsAdmHandler : AuthorizationHandler<IsAdmRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdmRequirement requirement)
    {
        if(context.User.HasClaim(claim => claim.ValueType == "roles" && claim.Value == "ADM"))
            context.Succeed(requirement);
        else
            context.Fail();

        return Task.CompletedTask;
    }
}
