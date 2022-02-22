using System.Collections.Generic;
using WoundHealingDb.Models;

namespace WoundHealingWebApi.Responses
{
    public class GetSurroundingSkinsResponse
    {
        public List<SurroundingSkin> SurroundingSkins { get; set; }
    }
}