namespace DotNet7JwtTest.Client.Shared;

public partial class NavMenu
{
    private bool collapseNavMenu = true;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;
}
