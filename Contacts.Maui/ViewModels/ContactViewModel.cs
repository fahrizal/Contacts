using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        public Contact Contact { get; set; }

        public ContactViewModel()
        {
            Contact = ContactRepository.GetContactById(1);
        }

        [RelayCommand]
        public void SaveContact()
        {
            ContactRepository.UpdateContact(Contact.ContactId, Contact);
        }

    }
}
