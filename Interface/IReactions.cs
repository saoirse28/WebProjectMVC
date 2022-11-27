using WebProjectMVC.Models;

namespace WebProjectMVC.Interface
{
    public interface IReactions
    {
        public Task<List<Reaction>> ReactionListAsync();
        public Task<Reaction> GetItemAsync(int? id);
        public Task<bool> CreateAsync(Reaction reaction);
        public Task<Reaction> EditViewAsync(int? id);
        public Task<Reaction> EditPostAsync(Reaction reaction);
        public Task<Reaction> DeleteItemAsync(int? id);
        public Task<bool> DeleteConfirmedAsync(int id);
    }
}
