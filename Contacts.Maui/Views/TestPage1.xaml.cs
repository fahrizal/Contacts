using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views;

public partial class TestPage1 : ContentPage
{
    private ContactViewModel contactViewModel;

    public TestPage1()
    {
        InitializeComponent();

        //contactViewModel = new ContactViewModel();
        //BindingContext = contactViewModel;
    }

}