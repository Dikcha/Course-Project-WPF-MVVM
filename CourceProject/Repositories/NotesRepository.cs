using CourceProject.Database;
using CourceProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceProject.Repositories
{
    public class NotesRepository
    {
        public async Task SaveUserNote(UserNotes userNote)
        {
            Notes Notes = new Notes();

            int month = userNote.NoteDate.Month;
            int day = userNote.NoteDate.Day;
            int year = userNote.NoteDate.Year;
            UserNotes note;
            if ((note = await Notes.UserNotes.Where(n => n.UserLogin.Equals(userNote.UserLogin) &&
                                                          n.NoteDate.Year.Equals(year) &&
                                                          n.NoteDate.Month.Equals(month) &&
                                                          n.NoteDate.Day.Equals(day))
                                .FirstOrDefaultAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(userNote.Note))
                    Notes.UserNotes.Remove(note);
                else
                {
                    note.Note = userNote.Note;
                    Notes.Entry(note).State = EntityState.Modified;
                }
            }
            else if (string.IsNullOrWhiteSpace(userNote.Note))
                return;
            else
            {
                UserNotes newNote = new UserNotes
                {
                    Note = userNote.Note,
                    UserLogin = userNote.UserLogin,
                    NoteDate = userNote.NoteDate
                };

                Notes.UserNotes.Add(newNote);
            }

            await Notes.SaveChangesAsync();
        }

        public async Task<List<UserNotes>> SearchNotesByText(string login, string searchedText)
        {
            Notes Notes = new Notes();

            List<UserNotes> userNotes = new List<UserNotes>();
            if (string.IsNullOrWhiteSpace(searchedText))
                return userNotes;

            foreach (var note in await Notes.UserNotes.Where(u => u.UserLogin.Equals(login)).ToListAsync())
            {
                if (note.Note.IndexOf(searchedText, StringComparison.Ordinal) >= 0)
                    userNotes.Add(note);
            }
            return userNotes;
        }

        public async Task<List<UserNotes>> GetNotesByLoginAndDate(string login, DateTime startDate, DateTime endDate)
        {
            Notes Notes = new Notes();

            return await Notes.UserNotes.Where(n => n.UserLogin.Equals(login))
                .Where(n => n.NoteDate >= startDate && n.NoteDate <= endDate)
                .ToListAsync();
        }
        public async Task<User> CheckUserCredentials(string login, string password)
        {
            Notes Notes = new Notes();

            foreach (var user in await Notes.User.ToListAsync())
            {
                if (string.CompareOrdinal(user.Login, login) == 0 && string.CompareOrdinal(user.Password, password) == 0)
                    return user;
            }
            return null;
        }
    }
}
