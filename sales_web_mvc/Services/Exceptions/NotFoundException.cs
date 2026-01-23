namespace sales_web_mvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        // excecao personalizada
        public NotFoundException(string message) : base(message) 
        {
        }
    }
}
