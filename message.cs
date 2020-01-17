using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyMentor.Server;

namespace Message
{
    public class Message
    {
        private string Sql_query { get; set; }
        public Message()
        {
        }
        public string ToJSON()
        {
            return JsonSerializer.Serialize(this);
        }
    }

}