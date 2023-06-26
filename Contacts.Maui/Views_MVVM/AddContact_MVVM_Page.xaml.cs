using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_MVVM;

public partial class AddContact_MVVM_Page : ContentPage
{
    private readonly ContactViewModel contactViewModel;

    public AddContact_MVVM_Page(ContactViewModel contactViewModel)
    {
        InitializeComponent();
        this.contactViewModel = contactViewModel;

        BindingContext = this.contactViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        contactViewModel.Contact = new CoreBusiness.Contact();
    }
}