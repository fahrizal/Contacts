using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private Contact contact;
        private readonly IViewContactUseCase viewContactUseCase;
        private readonly IEditContactUseCase editContactUseCase;
        private readonly IAddContactUseCase addContactUseCase;

        public Contact Contact
        {
            get => contact;
            set
            {
                SetProperty(ref contact, value);
            }
        }

        public bool IsNameProvided { get; set; }
        public bool IsEmailProvided { get; set; }
        public bool IsEmailFormatValid { get; set; }

        public ContactViewModel(IViewContactUseCase viewContactUseCase,
            IEditContactUseCase editContactUseCase, IAddContactUseCase addContactUseCase)
        {
            Contact = new Contact();
            this.viewContactUseCase = viewContactUseCase;
            this.editContactUseCase = editContactUseCase;
            this.addContactUseCase = addContactUseCase;
        }

        public async Task LoadContact(int contactId)
        {
            Contact = await viewContactUseCase.ExecuteAsync(contactId);
        }

        [RelayCommand]
        public async void EditContact()
        {
            if (await ValidateContact())
            {
                await editContactUseCase.ExecuteAsync(contact.ContactId, contact);
                await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
            }
        }

        [RelayCommand]
        public async void AddContact()
        {
            if (await ValidateContact())
            {
                await addContactUseCase.ExecuteAsync(contact);
                await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
            }
        }

        [RelayCommand]
        public async void BackToContacts()
        {
            await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
        }

        private async Task<bool> ValidateContact()
        {
            if (!IsNameProvided)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Name is required", "OK");
                return false;
            }

            if (!IsEmailProvided)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email is required", "OK");
                return false;
            }

            if (!IsEmailFormatValid)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email format is invalid", "OK");
                return false;
            }

            return true;

        }
    }
}
