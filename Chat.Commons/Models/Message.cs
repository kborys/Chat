namespace Chat.Commons.Models;

public class Message
{
    public int Sender { get; set; }
    public int Receiver { get; set; }
    public string Content { get; set; }
    public Message()
    {
        
    }
    public Message(int sender, int receiver, string content)
    {
        Sender = sender;
        Receiver = receiver;
        Content = content;
    }
}
