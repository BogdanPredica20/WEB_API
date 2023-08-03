using BogdanPredica_API.DataContext;
using BogdanPredica_API.Exceptions;
using BogdanPredica_API.Helpers.Enums;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API.Repositories
{
    public class MembersRepository : IMembersRepository
    {
        private readonly ClubLibraDataContext _context;

        public MembersRepository(ClubLibraDataContext context)
        {
            _context = context;
        }

        public async Task<Member> GetMemberByIdAsync(Guid id)
        {
            return await _context.Members.SingleOrDefaultAsync(a => a.IdMember == id);
        }

        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task CreateMemberAsync(Member member)
        {
            member.IdMember = Guid.NewGuid();

            bool username = await UsernameExists(member.Username);
            if (username)
            {
                throw new ModelValidationException(ErrorMessagesEnum.Member.UsernameExistsError);
            }

            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task<Member> UpdateMemberAsync(Guid id, Member member)
        {
            if (!await ExistMemberAsync(id))
            {
                return null;
            }

            if (member != null)
            {
                _context.Members.Update(member);
                await _context.SaveChangesAsync();
            }

            return member;
        }

        public async Task<Member> UpdatePartiallyMemberAsync(Guid id, Member member)
        {
            var memberFromDatabase = await GetMemberByIdAsync(id);
            bool memberIsChanged = false;

            if (memberFromDatabase == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(member.Name) && memberFromDatabase.Name != member.Name)
            {
                memberFromDatabase.Name = member.Name;
                memberIsChanged = true;
            }

            if (!string.IsNullOrEmpty(member.Title) && memberFromDatabase.Title != member.Title)
            {
                memberFromDatabase.Title = member.Title;
                memberIsChanged = true;
            }

            if (!string.IsNullOrEmpty(member.Description) && memberFromDatabase.Description != member.Description)
            {
                memberFromDatabase.Description = member.Description;
                memberIsChanged = true;
            }

            if (!string.IsNullOrEmpty(member.Position) && memberFromDatabase.Position != member.Position)
            {
                memberFromDatabase.Position = member.Position;
                memberIsChanged = true;
            }

            if (!string.IsNullOrEmpty(member.Resume) && memberFromDatabase.Resume != member.Resume)
            {
                memberFromDatabase.Resume = member.Resume;
                memberIsChanged = true;
            }

            if (!string.IsNullOrEmpty(member.Username) && memberFromDatabase.Username != member.Username)
            {
                memberFromDatabase.Username = member.Username;
                memberIsChanged = true;
            }

            if (!string.IsNullOrEmpty(member.Password) && memberFromDatabase.Password != member.Password)
            {
                memberFromDatabase.Password = member.Password;
                memberIsChanged = true;
            }

            if (!memberIsChanged)
            {
                throw new ModelValidationException(ErrorMessagesEnum.Announcement.ZeroUpdatesToSave);
            }

            _context.Update(memberFromDatabase);
            await _context.SaveChangesAsync();
            return memberFromDatabase;
        }

        public async Task<bool> DeleteMemberAsync(Guid id)
        {
            if (!await ExistMemberAsync(id))
            {
                return false;
            }

            if(await HasCodeSnippets(id))
            {
                return false;
            }

            if(await IsAssignedToAnyMembership(id))
            {
                return false;
            }

            _context.Members.Remove(new Member { IdMember = id });
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ExistMemberAsync(Guid id)
        {
            return await _context.Members.CountAsync(a => a.IdMember == id) > 0;
        }

        private async Task<bool> UsernameExists(string username)
        {
            return await _context.Members.CountAsync(a => a.Username == username) > 0;
        }

        private async Task<bool> HasCodeSnippets(Guid id)
        {
            return await _context.CodeSnippets.CountAsync(a => a.IdMember == id) > 0;
        }

        private async Task<bool> IsAssignedToAnyMembership(Guid id)
        {
            return await _context.Memberships.CountAsync(a => a.IdMember == id) > 0;
        }
    }
}
