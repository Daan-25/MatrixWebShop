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

            var customer = new Customer
            {
                Name = gebruiker.Gebruikersnaam,
                Address = "Onbekend",
                Active = true
            };

            gebruiker.Customer = customer;

            _context.Users.Add(gebruiker);
            _context.SaveChanges();
            return true;
        }

        public bool Inloggen(string gebruikersnaam, string wachtwoord, out int gebruikerId)
        {
            var gebruiker = _context.Users
                .FirstOrDefault(u => u.Gebruikersnaam == gebruikersnaam && u.Wachtwoord == wachtwoord);

            if (gebruiker != null)
            {
                gebruikerId = gebruiker.Id;
                return true;
            }

            gebruikerId = 0;
            return false;
        }
    }
}