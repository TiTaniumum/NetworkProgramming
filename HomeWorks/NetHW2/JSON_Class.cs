using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetHW2
{
    //https://api.post.kz/api/byOldPostcode/{postcode}?from={from}

    [Serializable]
    public class APIData
    {
        public List<AddressMainInfo>? Data { get; set; }
        public string? Total { get; set; }
        public string? From { get; set; }
    }
    [Serializable]
    public class AddressMainInfo
    {
        public string? PostCode { get; set; }
        public string? Rka { get; set; }
        public AddressType? Type { get; set; }
        public List<Part>? Parts { get; set; }
        public string? Number { get; set; }
        public string? OldPostcode { get; set; }
        public string? Address { get; set; }
        public string? Additional { get; set; }
        public string? AdditionalType { get; set; }
        public string? Corpus { get; set; }
        public string? CorpusNumber { get; set; }
        public string? Actual { get; set; }
        public string? Receive_index { get; set; }
        public string? Maintain_index { get; set; }
        public string? Delivery_index { get; set; }
        public string? PositionX { get; set; }
        public string? PositionY { get; set; }

    }
    [Serializable]
    public class AddressType
    {
        public string? ID { get; set; }
        public string? NameRus { get; set; }
        public string? NameKaz { get; set; }
    }
    [Serializable]
    public class Part
    {
        public string? ID { get; set; }
        public string? NameKaz { get; set; }
        public string? NameRus { get; set; }
        public string? TypeId { get; set; }
        public AddressType? Type { get; set; }
        public string? ParentId { get; set; }
        public string? Actual { get; set; }
    }
}
