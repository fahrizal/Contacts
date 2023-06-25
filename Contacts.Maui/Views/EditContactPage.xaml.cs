using Contacts.UseCases.Interfaces;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    private CoreBusiness.Contact contact;
    private readonly IViewContactUseCase viewContactUseCase;
    private readonly IEditContactUseCase editContactUseCase;

    public EditContactPage(IViewContactUseCase viewContactUseCase,
        IEditContactUseCase editContactUseCase)
    {
        InitializeComponent();
        this.viewContactUseCase = viewContactUseCase;
        this.editContactUseCase = editContactUseCase;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {
        contact.Name = contactControl.Name;
        contact.Email = contactControl.Email;
        contact.Phone = contactControl.Phone;
        contact.Address = contactControl.Address;

        await editContactUseCase.ExecuteAsync(contact.ContactId, contact);
        await Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }

    public string ContactId
    {
        set
        {

            contact = this.viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();

            if (contact != null)
            {
                contactControl.Name = contact.Name;
                contactControl.Email = contact.Email;
                contactControl.Phone = contact.Phone;
                contactControl.Address = contact.Address;
            }
        }
    }
}