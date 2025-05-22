namespace DataAccessLayer.Models
{
    public class UserService
    {
        private readonly MatrixIncDbContext _context;

        public UserService(MatrixIncDbContext context)
        {
            _context = context;
        }

        public bool Registreer(User gebruiker)
        {
            if (_context.Users.Any(u => u.Gebruikersnaam == gebruiker.Gebruikersnaam))
                return false;

            _context.Users.Add(gebruiker);
            _context.SaveChanges();
            return true;
        }

        public bool Inloggen(string gebruikersnaam, string wachtwoord)
        {
            return _context.Users.Any(u =>
                u.Gebruikersnaam == gebruikersnaam && u.Wachtwoord == wachtwoord);
        }
    }
}