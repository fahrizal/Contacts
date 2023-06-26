using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_MVVM;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContact_MVVM_Page : ContentPage
{
    private readonly ContactViewModel contactViewModel;

    public EditContact_MVVM_Page(ContactViewModel contactViewModel)
    {
        InitializeComponent();
        this.contactViewModel = contactViewModel;

        BindingContext = this.contactViewModel;
    }

    public string ContactId
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int contactId))
            {
                LoadContact(contactId);
            }
        }
    }

    private async void LoadContact(int contactId)
    {
        await contactViewModel.LoadContact(contactId);
    }
}