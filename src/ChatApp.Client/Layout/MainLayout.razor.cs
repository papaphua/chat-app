namespace ChatApp.Client.Layout;

public sealed partial class MainLayout
{
    private string? SearchValue { get; set; }
    private bool IsMenuOpen { get; set; }
    private bool IsDarkModeEnabled { get; set; } = true;

    private void ToggleMenu()
    {
        IsMenuOpen = !IsMenuOpen;
    }

    private void ToggleDarkMode()
    {
        IsDarkModeEnabled = !IsDarkModeEnabled;
    }
}