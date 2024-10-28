using Microsoft.AspNetCore.Components.Routing;

namespace PersonalRecipeManagerWebApp.Components.Layout
{
    public partial class NavMenu
    {
        bool sidebarExpanded = true;

        private Guid userId = new Guid("A40773D4-A9BC-49E8-AD9C-7908B732AD57");

        private string? currentUrl;

        protected override void OnInitialized()
        {
            currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
            StateHasChanged();
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
