using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiChoThue.Models
{
    public class DonHang
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string MaDonHang { get; set; }
        [BsonElement]
        public string TrangThai { get; set; }
        [BsonElement]
        public string ThoiGianDat { get; set; }
    }
}
