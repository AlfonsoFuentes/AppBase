﻿#nullable disable
namespace Client.Pages.Identity
{
    public partial class Forgot
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly ForgotPasswordRequest _emailModel = new();

        private async Task SubmitAsync()
        {
            var result = await _userManager.ForgotPasswordAsync(_emailModel);
            if (result.Succeeded)
            {
                _snackBar.Add("Done!", Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}