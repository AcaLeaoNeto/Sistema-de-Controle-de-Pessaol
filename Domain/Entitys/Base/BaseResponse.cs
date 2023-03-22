
namespace Domain.Entitys.Base
{
    public class BaseResponse 
    {
        public BaseResponse(int statusCode = 200, 
                                        string responseMessege = "Requisição Executada com Sucesso",
                                        object responseObject = null)
        {
            StatusCode = statusCode;
            ResponseMessage.Add(responseMessege);
            ResponseObject = responseObject;
        }


        public int StatusCode { get; set; }
        public List<string> ResponseMessage { get; set; } = new List<string>();
        public object ResponseObject { get; set; }
    }
}
