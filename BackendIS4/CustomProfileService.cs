namespace BackendIS4;

public class CustomProfileService : IProfileService
{
    public UserManager<IdentityUser>? C_UserManager { get; set; }

    public CustomProfileService(UserManager<IdentityUser>? p_UserManager)
    {
        C_UserManager = p_UserManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var m_Subject = context.Subject.GetSubjectId();
        var m_Usuario = await C_UserManager!.FindByIdAsync(m_Subject);
        if (m_Usuario == null)
            throw new Exception($"{nameof(m_Usuario)} é null!");

        var m_Claims = new List<Claim>
        {
            new("username", m_Usuario.UserName!),
            new("email", m_Usuario.Email!)
        };

        var m_Roles = await C_UserManager.GetRolesAsync(m_Usuario);
        foreach(var item in m_Roles)
            m_Claims.Add(new Claim("roles", item));

        context.IssuedClaims = m_Claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var m_Subject = context.Subject.GetSubjectId();
        var m_Usuario = await C_UserManager!.FindByIdAsync(m_Subject);

        context.IsActive = m_Usuario != null;
    }
}
