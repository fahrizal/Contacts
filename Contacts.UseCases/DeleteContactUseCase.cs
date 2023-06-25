using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;

namespace Contacts.UseCases
{
    public class DeleteContactUseCase : IDeleteContactUseCase
    {
        private readonly IContactRepository contactRepository;

        public DeleteContactUseCase(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task ExecuteAsync(int contactId)
        {
            await this.contactRepository.DeleteContactAsync(contactId);
        }
    }
}
