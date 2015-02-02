namespace Gabbler.gApi.Models
{
    public class MsgModel
    {
        public MsgModel()
        {
            
        }
        public MsgModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}