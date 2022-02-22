using MediatR;
using Newtonsoft.Json;
using System;
using WoundHealingWebApi.Enums;

namespace WoundHealingWebApi.Requests
{
    public class CreateUserRequest : IRequest
    {
        public AccountType AccountType { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Pesel { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Salt { get; set; }
        public string Hash { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}