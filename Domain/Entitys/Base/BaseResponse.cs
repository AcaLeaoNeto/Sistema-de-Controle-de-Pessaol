
namespace Domain.Entitys.Base
{
    public class BaseResponse 
    {
        public BaseResponse(int statusCode = 200, 
                                        string responseMessege = "Requisição Executada com Sucesso",
                                        object responseObject = null)
        {
            StatusCode = statusCode;
            ResponseMessage = responseMessege;
            ResponseObject = responseObject;
        }


        public int StatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponseObject { get; set; }
    }
}
