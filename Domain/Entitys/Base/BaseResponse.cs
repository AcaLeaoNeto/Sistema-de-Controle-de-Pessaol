
namespace Domain.Entitys.Base
{
    public class BaseResponse
    {
        public BaseResponse(int statusCode = 200, string requestMessege = "Requisição Executada com Sucesso")
        {
            StatusCode = statusCode;
            RequestMessage = requestMessege;
        }


        public int StatusCode { get; }
        public string RequestMessage { get; }

    }
}
