using Domain.Notifications;

namespace Domain.Entitys.Login
{
    public class LoginSingOn
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;


        public bool Validation(INotification notification)
        {
            if (!PasswordSizeValid(Password))
                notification.AddMessage("Senha deve possuir pelo menos 8 caracteres.");

            if (!PasswordDigitValid(Password))
                notification.AddMessage("Senha deve conter pelo menos um numero");

            if (!PasswordLetterValid(Password))
                notification.AddMessage("Senha deve conter pelo menos uma letra");

            if (!PasswordUpLetterValid(Password))
                notification.AddMessage("Senha deve conter pelo menos um numero maiúscula");

            if (!PasswordSpecialValid(Password))
                notification.AddMessage("Senha deve conter pelo menos um carácter especial");

            if (!RoleValid(Role))
                notification.AddMessage("Role Permitidos são 'Regular' ou 'Manager'");

            return notification.Valid;
        }

        private bool PasswordSizeValid(string password) 
        {
            return password.Length >= 8;
        }

        private bool PasswordDigitValid(string password)
        {
            return password.Any(c => char.IsDigit(c));
        }

        private bool PasswordLetterValid(string password)
        {
            return password.Any(c => char.IsLetter(c));
        }

        private bool PasswordUpLetterValid(string password)
        {
            return password.Any(c => char.IsUpper(c));
        }

        private bool PasswordSpecialValid(string password)
        {
            return password.Any(c => !Char.IsLetterOrDigit(c));
        }

        private bool RoleValid(string Role)
        {
            return Role == "Manager" || Role == "Regular";
        }


    }
}
