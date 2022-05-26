namespace TTGServer.Models.ServiceModels
{
    public class DeletePersonalInfoVerification
    {
        public string Login { get; set; }
        public char Category { get; set; }
        public string Password { get; set; }
    }
}
